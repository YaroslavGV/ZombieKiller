# Zombie Filler
[APK](https://drive.google.com/file/d/1U59CqQhlH9vmP5sWslQEDpr-yxxZ-WXe/view?usp=sharing)

![Game](https://i.imgur.com/UMUHiI5.png)

Используется из AssetStore:
- [Extenject](https://assetstore.unity.com/packages/tools/utilities/extenject-dependency-injection-ioc-157735)
- [JSON Object](https://assetstore.unity.com/packages/tools/input-management/json-object-710)
- [Fast IK](https://assetstore.unity.com/packages/tools/animation/fast-ik-139972)

## Map
![TileMap](https://i.imgur.com/6FR9O4t.png)

Карта реализована с помощью `TileMap`

## Gameplay UI
![GameUI](https://i.imgur.com/5iVjhvQ.png)

- (1) аналог движения
- (2) кнопка стрельбы
- (3) кнопка инвенторя
- (4) количество патронов

## Inventory
![Inventory](https://i.imgur.com/ViwK7ET.png)

![DeleteAmount](https://i.imgur.com/tZNPuvf.png)

- предметы можно удалять
- для `StackableItem` можно выбрать количество удаляемых предметов
- `ItemEquipment` можно экиперовать

Реализация: `Game/Service/Inventory`
Визуализация: `Game/GamplayUI/Inventory`

## Save/Load
![JsonHandler](https://i.imgur.com/3a2Gmmu.png)

Сохранение реализуется интерфейсом `IJsonHandler`. Есть две реализации: `PlayerPrefshandler` и `PersistentDataHandler`. 
Все агенты загрузки и сохранения добавляются в обработчик как `IJsonHandle`. Сохранение происходит при переходе в следующий раунд.

## Gameplay

### UnitModel
![UnitTemplate](https://i.imgur.com/u0rQz6l.png)

Модель юнитов инициализируется по `IUnitProfile`.
Модель обрабатывает урон, используя `DamageDroducer` и `DamageHandler`, всю остальную логику, как например движение реализовывают модули `UnitModul`.

### UnitSkin
Скин так-же имеет модули, занимающиеся анимациями движения, мерти, флипа по X. Скин человека имеет уникальный модуль для IK рук. Так-же скин имеет свой набор точек, как например точка для полоски сдоровья.

### UnitProfile
![Profile](https://i.imgur.com/4Rapjmn.png)

В профиле содержится информация о статах, скине, экиперовке, патронов и другом.
Имеется две реализации, `UnitProfile` и `PlayerProfile`, в котором экиперовка не задана, а патроны берутя из инвенторя.

### InputValues
Все юниты управляются через значения `UnitModel.Inputs`.
 Движение и атаку игрока указывает `PlayerInputhandler`, а для врагов `AIInputhandler`. `target` для юнитов определяется `UnitModelTarget`. `attackRange` определяетс `UnitWeaponModul` согласно экиперованному оружию.

### Bullet
![BulletAsset](https://i.imgur.com/xQib2w0.png)

Патроны имеют ассет `BulletTemplateAsset`, где указан темплейт пули, теплейт эффектов и указано событие для них. 
Все пули пулируются в `SceneBullets`. Оружие как `IBulletAssetUser` добавляются в `SceneBullets` и получает `BulletHandler` согласно ассету который использует, который занимается жизненным циклом пуль. `BulletHandler` является частью `BulletAssetHandler`, который так-же создает пуллы эффектов `BulletEffectHandler` занимающихся жизненным циклом эффектов для пуль. `UsageBulletAssetHandler` наследуемый от `BulletAssetHandler` считает количество юзеров и удаляется если их нет, все юзеры, которые используют одинаковые пули, используют один и тот-же пулл.

### Drop
![DropTable](https://i.imgur.com/VEXlV75.png)

Собираемые предметы реализуют `ICollectableDrop`. Для подбора каждого типа дропа реализуется свой `IDropCollector`. Юнит игрока содержит `DropCollector` являющимся фасадом для набора разных коллекторов, для предметов `CollectorItem`, добавляющий их в инвентаря, а для хилок `CollectorHealth` воостанавливающий юниту жизни.
У каждого юнита уникальный дроп описанный в профиле в виде `DropTable`.