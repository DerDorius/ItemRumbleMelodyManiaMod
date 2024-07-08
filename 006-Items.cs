using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Items
{
    private ModObjectContext modContext;
    private ItemRumbleModModSettings modSettings;

    public Items(ModObjectContext modContext, ItemRumbleModModSettings itemRumbleModModSettings)
    {
        this.modContext = modContext;
        this.modSettings = itemRumbleModModSettings;
        SetupSpawnProbabilities();
    }

    public static readonly Item Coin = new Item("Coin")
    {
        Description = "Adds 100 Points",
        ImagePath = "images/items/coins/Gold_1.png",
        VisualElementName = "itemCollectorItem",

        // Has to be object, because of loading order of cs files.
        OnCollectAction = (object itemActionsObject, object itemControlObject) =>
        {
            // Cast the object to the correct type.
            var itemActions = (ItemActions)itemActionsObject;
            var itemControl = (ItemControl)itemControlObject;

            // Get the PlayerControl of the affected player.
            PlayerControl targetPlayerControl = itemActions.GetMyPlayerControll();

            // Add Actions here.
            itemActions.AddScore(targetPlayerControl, 100);
            itemActions.BouncePlayerScoreLabel(targetPlayerControl);
            itemActions.AnimateItemCollection(targetPlayerControl, itemControl, "+100 Coin");
        },
    };

    public static readonly Item Banana = new Item("Banana")
    {
        Description = "Subtracts 75 Points",
        ImagePath = "images/items/banana/fruit_banana.png",
        VisualElementName = "itemCollectorItem",
        // Has to be object, because of loading order of cs files.
        OnCollectAction = (object itemActionsObject, object itemControlObject) =>
        {
            // Cast the object to the correct type.
            var itemActions = (ItemActions)itemActionsObject;
            var itemControl = (ItemControl)itemControlObject;
            // Get the PlayerControl of the affected player.
            PlayerControl targetPlayerControl = itemActions.GetMyPlayerControll();
            // Add Actions here.
            itemActions.AddScore(targetPlayerControl, -75);
            itemActions.BouncePlayerScoreLabel(targetPlayerControl);
            itemActions.AnimateItemCollection(targetPlayerControl, itemControl, "-75 Banana");
        },
    };

    public static readonly Item BlueShelli = new Item("Blue Shelli")
    {
        Description = "Deducts 250 Points from the player in first place",
        ImagePath = "images/items/shells/blue_shell.png",
        VisualElementName = "shellCollectorItem",
        // Has to be object, because of loading order of cs files.
        OnCollectAction = (object itemActionsObject, object itemControlObject) =>
        {
            // Cast the object to the correct type.
            var itemActions = (ItemActions)itemActionsObject;
            var itemControl = (ItemControl)itemControlObject;
            // Get the PlayerControl of the affected player.
            PlayerControl targetPlayerControl = itemActions.GetFirstPlacePlayerControl();
            // Add Actions here.
            itemActions.AddScore(targetPlayerControl, -250);
            itemActions.BouncePlayerScoreLabel(targetPlayerControl);
            itemActions.AnimateItemCollection(targetPlayerControl, itemControl, "-250 Blue Shell");
        },
    };

    public static readonly Item GreenShelli = new Item("Green Shelli")
    {
        Description = "Deducts 50 points from a random player",
        ImagePath = "images/items/shells/green_shell.png",
        VisualElementName = "shellCollectorItem",
        // Has to be object, because of loading order of cs files.
        OnCollectAction = (object itemActionsObject, object itemControlObject) =>
        {
            // Cast the object to the correct type.
            var itemActions = (ItemActions)itemActionsObject;
            var itemControl = (ItemControl)itemControlObject;
            // Get the PlayerControl of the affected player.
            PlayerControl targetPlayerControl = itemActions.GetRandomPlayerControl();
            // Add Actions here.
            itemActions.AddScore(targetPlayerControl, -50);
            itemActions.BouncePlayerScoreLabel(targetPlayerControl);
            itemActions.AnimateItemCollection(targetPlayerControl, itemControl, "-50 Green Shell");
        },
    };

    public static readonly Item RedShelli = new Item("Red Shelli")
    {
        Description = "Deducts 75 points the player in front of you",
        ImagePath = "images/items/shells/red_shell.png",
        VisualElementName = "shellCollectorItem",
        // Has to be object, because of loading order of cs files.
        OnCollectAction = (object itemActionsObject, object itemControlObject) =>
        {
            // Cast the object to the correct type.
            var itemActions = (ItemActions)itemActionsObject;
            var itemControl = (ItemControl)itemControlObject;
            // Get the PlayerControl of the affected player.
            PlayerControl targetPlayerControl = itemActions.GetInFrontOfMePlayerControl();
            // Add Actions here.
            itemActions.AddScore(targetPlayerControl, -75);
            itemActions.BouncePlayerScoreLabel(targetPlayerControl);
            itemActions.AnimateItemCollection(targetPlayerControl, itemControl, "-75 Red Shell");
        },
    };

    public static readonly Item Flashi = new Item("Flash")
    {
        Description = "Mutes audio for 2 seconds",
        ImagePath = "images/items/lightning.png",
        VisualElementName = "shellCollectorItem",
        // Has to be object, because of loading order of cs files.
        OnCollectAction = (object itemActionsObject, object itemControlObject) =>
        {
            // Cast the object to the correct type.
            var itemActions = (ItemActions)itemActionsObject;
            var itemControl = (ItemControl)itemControlObject;
            // Get the PlayerControl of the affected player.
            PlayerControl targetPlayerControl = itemActions.GetMyPlayerControll();
            // Add Actions here.
            itemActions.MuteAudio(2);
            itemActions.MoveToCenterAndFadeOut(
                itemControl,
                targetPlayerControl,
                0.8f,
                () =>
                {
                    itemActions.ShowItemRating(targetPlayerControl, "Flashi");
                }
            );
        },
    };

    public static readonly Item Ghosti = new Item("Ghost")
    {
        Description = "Makes lyrics invisible for 5 seconds",
        ImagePath = "images/items/ghost.png",
        VisualElementName = "shellCollectorItem",
        // Has to be object, because of loading order of cs files.
        OnCollectAction = (object itemActionsObject, object itemControlObject) =>
        {
            // Cast the object to the correct type.
            var itemActions = (ItemActions)itemActionsObject;
            var itemControl = (ItemControl)itemControlObject;
            // Get the PlayerControl of the affected player.
            PlayerControl targetPlayerControl = itemActions.GetMyPlayerControll();
            // Add Actions here.
            itemActions.DisableLyricsForSeconds(5);
            itemActions.MoveToCenterAndFadeOut(
                itemControl,
                targetPlayerControl,
                0.8f,
                () =>
                {
                    itemActions.ShowItemRating(targetPlayerControl, "Ghosti");
                }
            );
        },
    };

    public static readonly Item Mushi = new Item("Mushi Mushroom")
    {
        Description = "Gives you 250 points",
        ImagePath = "images/items/mushroom.png",
        VisualElementName = "shellCollectorItem",
        // Has to be object, because of loading order of cs files.
        OnCollectAction = (object itemActionsObject, object itemControlObject) =>
        {
            // Cast the object to the correct type.
            var itemActions = (ItemActions)itemActionsObject;
            var itemControl = (ItemControl)itemControlObject;
            // Get the PlayerControl of the affected player.
            PlayerControl targetPlayerControl = itemActions.GetMyPlayerControll();
            // Add Actions here.
            itemActions.AddScore(targetPlayerControl, 250);
            itemActions.BouncePlayerScoreLabel(targetPlayerControl);
            itemActions.AnimateItemCollection(
                targetPlayerControl,
                itemControl,
                "+250 Mushi Mushroom"
            );
        },
    };

    public static readonly Item Rocki = new Item("Rocki Rocket")
    {
        Description = "Gives you 500 points",
        ImagePath = "images/items/rocket.png",
        VisualElementName = "shellCollectorItem",
        // Has to be object, because of loading order of cs files.
        OnCollectAction = (object itemActionsObject, object itemControlObject) =>
        {
            // Cast the object to the correct type.
            var itemActions = (ItemActions)itemActionsObject;
            var itemControl = (ItemControl)itemControlObject;
            // Get the PlayerControl of the affected player.
            PlayerControl targetPlayerControl = itemActions.GetMyPlayerControll();
            // Add Actions here.
            itemActions.AddScore(targetPlayerControl, 500);
            itemActions.BouncePlayerScoreLabel(targetPlayerControl);
            itemActions.AnimateItemCollection(
                targetPlayerControl,
                itemControl,
                "+500 Rocki Rocket"
            );
        },
    };

    public static readonly Item Stari = new Item("Star")
    {
        Description = "Speeds up the song for 3 seconds",
        ImagePath = "images/items/star.png",
        VisualElementName = "shellCollectorItem",
        // Has to be object, because of loading order of cs files.
        OnCollectAction = (object itemActionsObject, object itemControlObject) =>
        {
            // Cast the object to the correct type.
            var itemActions = (ItemActions)itemActionsObject;
            var itemControl = (ItemControl)itemControlObject;
            // Get the PlayerControl of the affected player.
            PlayerControl targetPlayerControl = itemActions.GetMyPlayerControll();
            // Add Actions here.
            itemActions.ChangePlaybackSpeed(3, 1.15f);
            itemActions.MoveToCenterAndFadeOut(
                itemControl,
                targetPlayerControl,
                0.8f,
                () =>
                {
                    itemActions.ShowItemRating(targetPlayerControl, "Stari Speed Up");
                }
            );
        },
    };

    public static readonly Item Snaili = new Item("Snail")
    {
        Description = "Slows down the song for 3 seconds",
        ImagePath = "images/items/snaili.png",
        VisualElementName = "shellCollectorItem",
        // Has to be object, because of loading order of cs files.
        OnCollectAction = (object itemActionsObject, object itemControlObject) =>
        {
            // Cast the object to the correct type.
            var itemActions = (ItemActions)itemActionsObject;
            var itemControl = (ItemControl)itemControlObject;
            // Get the PlayerControl of the affected player.
            PlayerControl targetPlayerControl = itemActions.GetMyPlayerControll();
            // Add Actions here.
            itemActions.ChangePlaybackSpeed(3, 0.85f);
            itemActions.MoveToCenterAndFadeOut(
                itemControl,
                targetPlayerControl,
                0.9f,
                () =>
                {
                    itemActions.ShowItemRating(targetPlayerControl, "Snaili Slow Down");
                }
            );
        },
    };

    public static readonly Item Eraser = new Item("Note Eraser")
    {
        Description = "Hides the Notes of the Collecting Player for 5 seconds",
        ImagePath = "images/items/eraser.png",
        VisualElementName = "shellCollectorItem",
        // Has to be object, because of loading order of cs files.
        OnCollectAction = (object itemActionsObject, object itemControlObject) =>
        {
            // Cast the object to the correct type.
            var itemActions = (ItemActions)itemActionsObject;
            var itemControl = (ItemControl)itemControlObject;
            // Get the PlayerControl of the affected player.
            List<PlayerControl> targetPlayerControls = itemActions.GetPlayerControlsInFrontOfMe();
            PlayerControl collectingPlayerControl = itemActions.GetMyPlayerControll();

            // Add Actions here.
            foreach (PlayerControl playerControl in targetPlayerControls)
            {
                itemActions.HideNotesForSeconds(playerControl, 5);
            }
            if (targetPlayerControls.Count == 0)
            {
                itemActions.HideNotesForSeconds(collectingPlayerControl, 5);
            }

            itemActions.AnimateItemCollection(collectingPlayerControl, itemControl, "Note Eraser");
        },
    };

    public static readonly Item TinaTurner = new Item("Tina Turner")
    {
        Description = "Rotates the Screen 360°",
        ImagePath = "images/items/rotateScreen.png",
        VisualElementName = "shellCollectorItem",
        // Has to be object, because of loading order of cs files.
        OnCollectAction = (object itemActionsObject, object itemControlObject) =>
        {
            // Cast the object to the correct type.
            var itemActions = (ItemActions)itemActionsObject;
            var itemControl = (ItemControl)itemControlObject;
            // Get the PlayerControl of the affected player.
            PlayerControl targetPlayerControl = itemActions.GetMyPlayerControll();
            // Add Actions here.
            itemActions.rotateScreen(2);
            itemActions.MoveToCenterAndFadeOut(
                itemControl,
                targetPlayerControl,
                0.9f,
                () =>
                {
                    itemActions.ShowItemRating(targetPlayerControl, "Tina Turner");
                }
            );
        },
    };

    public static readonly Item Shaker = new Item("Shaker")
    {
        Description = "Shakes the Screen for 3 seconds",
        ImagePath = "images/items/shakescreen.png",
        VisualElementName = "shellCollectorItem",
        // Has to be object, because of loading order of cs files.
        OnCollectAction = (object itemActionsObject, object itemControlObject) =>
        {
            // Cast the object to the correct type.
            var itemActions = (ItemActions)itemActionsObject;
            var itemControl = (ItemControl)itemControlObject;
            // Get the PlayerControl of the affected player.
            PlayerControl targetPlayerControl = itemActions.GetMyPlayerControll();
            // Add Actions here.
            itemActions.shakeScreen(2, 1.3f);
            itemActions.MoveToCenterAndFadeOut(
                itemControl,
                targetPlayerControl,
                0.9f,
                () =>
                {
                    itemActions.ShowItemRating(targetPlayerControl, "Shaker");
                }
            );
        },
    };

    public static readonly List<Item> AllItems = new List<Item>()
    {
        Coin,
        Banana,
        BlueShelli,
        GreenShelli,
        RedShelli,
        Flashi,
        Ghosti,
        Mushi,
        Rocki,
        Stari,
        Snaili,
        Eraser,
        TinaTurner,
        Shaker
    };
    private Dictionary<string, Dictionary<int, float>> spawnProbsPerItem =
        new Dictionary<string, Dictionary<int, float>>();

    public void SetupSpawnProbabilities()
    {
        // Read them from SpawnProbabilities.csv

        FileStream fileStream = new FileStream(
            Path.Combine(modContext.ModPersistentDataFolder, "SpawnProbabilities.csv"),
            FileMode.Open,
            FileAccess.Read,
            FileShare.ReadWrite
        ); // so we can edit the file while the game is running
        StreamReader fileReader = new StreamReader(fileStream);
        List<string> lines = new List<string>();

        while (!fileReader.EndOfStream)
        {
            lines.Add(fileReader.ReadLine());
        }

        fileReader.Close();
        fileStream.Close();

        string[] csv = lines.ToArray();
        string[] header = csv[0].Split(';');
        string[] itemNames = header.Skip(1).ToArray(); // Skip the first two columns

        if (itemNames.Length != AllItems.Count)
        {
            NotificationManager.CreateNotification(
                Translation.Of(
                    "Overwriting SpawnProbabilities.csv because the number of items has changed."
                )
            );
            NotificationManager.CreateNotification(
                Translation.Of(
                    "Created a backup of the old SpawnProbabilities.csv in the mod's persistent data folder."
                )
            );
            // Create Backup
            File.Copy(
                Path.Combine(modContext.ModPersistentDataFolder, "SpawnProbabilities.csv"),
                Path.Combine(modContext.ModPersistentDataFolder, "SpawnProbabilities.csv.bak")
            );
            File.Copy(
                Path.Combine(modContext.ModFolder, "SpawnProbabilities.csv"),
                Path.Combine(modContext.ModPersistentDataFolder, "SpawnProbabilities.csv")
            );
            SetupSpawnProbabilities();
            return;
        }

        foreach (string line in csv.Skip(1)) // Skip the header line
        {
            string[] values = line.Split(';');
            int pointDistance = int.Parse(values[0]);

            for (int i = 0; i < itemNames.Length; i++)
            {
                string itemName = itemNames[i];
                float probability = float.Parse(values[i + 1]); // Skip the first two columns

                if (!spawnProbsPerItem.ContainsKey(itemName))
                {
                    spawnProbsPerItem[itemName] = new Dictionary<int, float>();
                }
                //Debug.Log("Adding " + itemName + " with " + pointDistance + " and " + probability);
                spawnProbsPerItem[itemName][pointDistance] = probability;
            }
        }

        // Check if all items are in the dictionary
        foreach (Item item in AllItems)
        {
            if (!spawnProbsPerItem.ContainsKey(item.Name))
            {
                Debug.LogError("Item " + item.Name + " is not in the SpawnProbabilities.csv");
                NotificationManager.CreateNotification(
                    Translation.Of(
                        "Item "
                            + item.Name
                            + " is not in the SpawnProbabilities.csv, Is there a typo?"
                    )
                );
            }
        }
    }

    /**
     * Returns a random item based on the pointsToFirstPlace.
     */
    public Item SpawnItem(int pointsToFirstPlace, List<Item> activeItems)
    {
        try
        {
            Dictionary<Item, int> itemProbabilities = new Dictionary<Item, int>();
            foreach (Item item in activeItems)
            {
                // find nearest key in dictionary
                int nearestKey = 0;
                foreach (int key in spawnProbsPerItem[item.Name].Keys)
                {
                    if (
                        Math.Abs(key - pointsToFirstPlace)
                        < Math.Abs(nearestKey - pointsToFirstPlace)
                    )
                    {
                        nearestKey = key;
                    }
                }

                int probability = (int)spawnProbsPerItem[item.Name][nearestKey];
                itemProbabilities.Add(item, probability);
            }

            // monte carlo method
            int sum = 0;
            foreach (int probability in itemProbabilities.Values)
            {
                sum += probability;
            }
            int randomValue = UnityEngine.Random.Range(0, sum);
            int currentSum = 0;
            foreach (Item item in itemProbabilities.Keys)
            {
                currentSum += itemProbabilities[item];
                if (randomValue <= currentSum)
                {
                    return item;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        return Coin;
    }
}
