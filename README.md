# Zombie Filler
[APK](https://drive.google.com/file/d/1U59CqQhlH9vmP5sWslQEDpr-yxxZ-WXe/view?usp=sharing)

![Game](https://i.imgur.com/UMUHiI5.png)

������������ �� AssetStore:
- [Extenject](https://assetstore.unity.com/packages/tools/utilities/extenject-dependency-injection-ioc-157735)
- [JSON Object](https://assetstore.unity.com/packages/tools/input-management/json-object-710)
- [Fast IK](https://assetstore.unity.com/packages/tools/animation/fast-ik-139972)

## Map
![TileMap](https://i.imgur.com/6FR9O4t.png)

����� ����������� � ������� `TileMap`

## Gameplay UI
![GameUI](https://i.imgur.com/5iVjhvQ.png)

- (1) ������ ��������
- (2) ������ ��������
- (3) ������ ���������
- (4) ���������� ��������

## Inventory
![Inventory](https://i.imgur.com/ViwK7ET.png)

![DeleteAmount](https://i.imgur.com/tZNPuvf.png)

- �������� ����� �������
- ��� `StackableItem` ����� ������� ���������� ��������� ���������
- `ItemEquipment` ����� �����������

����������: `Game/Service/Inventory`
������������: `Game/GamplayUI/Inventory`

## Save/Load
![JsonHandler](https://i.imgur.com/3a2Gmmu.png)

���������� ����������� ����������� `IJsonHandler`. ���� ��� ����������: `PlayerPrefshandler` � `PersistentDataHandler`. 
��� ������ �������� � ���������� ����������� � ���������� ��� `IJsonHandle`. ���������� ���������� ��� �������� � ��������� �����.

## Gameplay

### UnitModel
![UnitTemplate](https://i.imgur.com/u0rQz6l.png)

������ ������ ���������������� �� `IUnitProfile`.
������ ������������ ����, ��������� `DamageDroducer` � `DamageHandler`, ��� ��������� ������, ��� �������� �������� ������������� ������ `UnitModul`.

### UnitSkin
���� ���-�� ����� ������, ������������ ���������� ��������, �����, ����� �� X. ���� �������� ����� ���������� ������ ��� IK ���. ���-�� ���� ����� ���� ����� �����, ��� �������� ����� ��� ������� ��������.

### UnitProfile
![Profile](https://i.imgur.com/4Rapjmn.png)

� ������� ���������� ���������� � ������, �����, ����������, �������� � ������.
������� ��� ����������, `UnitProfile` � `PlayerProfile`, � ������� ���������� �� ������, � ������� ������ �� ���������.

### InputValues
��� ����� ����������� ����� �������� `UnitModel.Inputs`.
 �������� � ����� ������ ��������� `PlayerInputhandler`, � ��� ������ `AIInputhandler`. `target` ��� ������ ������������ `UnitModelTarget`. `attackRange` ����������� `UnitWeaponModul` �������� �������������� ������.

### Bullet
![BulletAsset](https://i.imgur.com/xQib2w0.png)

������� ����� ����� `BulletTemplateAsset`, ��� ������ �������� ����, ������� �������� � ������� ������� ��� ���. 
��� ���� ���������� � `SceneBullets`. ������ ��� `IBulletAssetUser` ����������� � `SceneBullets` � �������� `BulletHandler` �������� ������ ������� ����������, ������� ���������� ��������� ������ ����. `BulletHandler` �������� ������ `BulletAssetHandler`, ������� ���-�� ������� ����� �������� `BulletEffectHandler` ������������ ��������� ������ �������� ��� ����. `UsageBulletAssetHandler` ����������� �� `BulletAssetHandler` ������� ���������� ������ � ��������� ���� �� ���, ��� �����, ������� ���������� ���������� ����, ���������� ���� � ���-�� ����.

### Drop
![DropTable](https://i.imgur.com/VEXlV75.png)

���������� �������� ��������� `ICollectableDrop`. ��� ������� ������� ���� ����� ����������� ���� `IDropCollector`. ���� ������ �������� `DropCollector` ���������� ������� ��� ������ ������ �����������, ��� ��������� `CollectorItem`, ����������� �� � ���������, � ��� ����� `CollectorHealth` ����������������� ����� �����.
� ������� ����� ���������� ���� ��������� � ������� � ���� `DropTable`.