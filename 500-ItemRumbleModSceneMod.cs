using System;
using System.Collections.Generic;
using System.IO;
using UniInject;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemRumbleModSceneMod : IGameRoundMod
{
    [Inject]
    private ModObjectContext modContext;

    [Inject]
    private ItemRumbleModModSettings modSettings;

    [Inject]
    private UiManager uiManager;

    public List<String> activeItems = new List<String>();

    public string DisplayName => "Item Rumble";

    public double DisplayOrder => 0;

    private ItemRumbleGameRoundModifierControl control;

    public GameRoundModifierControl CreateControl()
    {
        control =
            GameObjectUtils.CreateGameObjectWithComponent<ItemRumbleGameRoundModifierControl>();
        control.modContext = modContext;
        control.itemRumbleModModSettings = modSettings;
        return control;
    }

    public VisualElement CreateConfigurationVisualElement()
    {
        activeItems = new List<string>(modSettings.activeItemList.Split(','));
        Debug.Log("Active Items: " + string.Join(",", activeItems));
        Debug.Log("Item coutn" + activeItems.Count);
        if (activeItems.Count == 1)
        {
            Debug.Log("No active items found, adding all items to active list");
            foreach (Item item in Items.AllItems)
            {
                activeItems.Add(item.Name);
                modSettings.activeItemList = string.Join(",", activeItems);
            }
        }

        VisualElement parent = new VisualElement();
        var visualElement = new VisualElement();
        var label = new Label("Item Rumble Mod Settings");
        label.style.fontSize = 10;
        visualElement.Add(label);
        var label2 = new Label("Consider Contributing to the Mod on Github!");
        label2.style.fontSize = 7;
        visualElement.Add(label2);
        StyleColor grey = new StyleColor();
        grey.value = new Color(0.5f, 0.5f, 0.5f, 1f);
        var divider = new VisualElement();
        divider.style.height = 1;

        divider.style.backgroundColor = grey; //grey
        visualElement.Add(divider);
        foreach (Item item in Items.AllItems)
        {
            var row = new VisualElement();
            row.style.flexDirection = FlexDirection.Row;
            row.style.flexGrow = 1;
            row.style.justifyContent = Justify.SpaceBetween;

            // create a checkbox for each item
            IModSettingControl checkbox = new BoolModSettingControl(
                () => activeItems.Contains(item.Name),
                newValue =>
                {
                    if (newValue)
                    {
                        activeItems.Add(item.Name);
                    }
                    else
                    {
                        activeItems.Remove(item.Name);
                    }
                    modSettings.activeItemList = string.Join(",", activeItems);
                }
            )
            {
                Label = item.Name
            };
            row.Add(checkbox.CreateVisualElement());
            var image = new VisualElement();
            image.style.width = 25;
            image.style.height = 25;
            ImageManager
                .LoadSpriteFromUri($"{modContext.ModFolder}/{item.ImagePath}")
                .Subscribe(sprite => image.style.backgroundImage = new StyleBackground(sprite));
            row.Add(image);
            visualElement.Add(row);

            var itemLabel = new Label(item.Description);
            itemLabel.style.fontSize = 7;
            visualElement.Add(itemLabel);
            var itemDivider = new VisualElement();
            itemDivider.style.height = 1;
            itemDivider.style.backgroundColor = grey; //grey
            visualElement.Add(itemDivider);
        }

        IntModSettingControl intModSettingControl = new IntModSettingControl(
            () => modSettings.percentChanceToSpawnItem,
            newValue => modSettings.percentChanceToSpawnItem = newValue
        )
        {
            Label = "Spawn probability for item on each note (%)"
        };
        visualElement.Add(intModSettingControl.CreateVisualElement());
        Button probChangeHelpButton = new Button(() =>
        {
            uiManager.CreateInfoDialogControl(
                Translation.Of("Editing Spawn Probabilities"),
                Translation.Of(
                    "Spawn probabilities are stored in a CSV file. The first column is the distance to the leading player. After that each column represents the probability for the item at the corresponding distance. The file is located in the mod folder. You can open it with Excel or any text editor."
                )
            );
        });
        probChangeHelpButton.text = "How to Edit Spawn Probabilities?";
        probChangeHelpButton.AddToClassList("my-2");
        visualElement.Add(probChangeHelpButton);

        Button openProbFileButton = new Button(() =>
        {
            Application.OpenURL(
                Path.Combine(modContext.ModPersistentDataFolder, "SpawnProbabilities.csv")
            );
        });
        openProbFileButton.text = "Edit Spawn Probabilities (Excel CSV)";

        visualElement.Add(openProbFileButton);
        parent.Add(visualElement);
        return parent;
    }
}
