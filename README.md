# ⚽⚽⚽BallGamesWindowsFormApp
⚽Четыре небольших проекта, написанных в процессе изучения инструментов рисования и движения в WinForms и принципов ООП

## 🔴BallGameWindowsFormApp

## 📝Описание работы
1. Есть кнопка "Создать", которая создает на форме более 5 шариков, которые начинают двигаться в разные стороны и с разной скоростью.
2. Есть кнопка "Остановить", которая останавливает все созданные шарики.
3. Считается, что шарик пойман, если он полностью находится на форме.
4. После остановки шариков, выдайте пользователю количество шариков, которые удалось поймать.

### 🕹️Геймплей
Пример работы приложения приведён ниже:
<div " align="center">

![BallGamesWindowsFormApp](https://github.com/IvanPovaliaev/BallGamesWindowsFormApp/assets/157638990/4e13057a-0885-44d9-8d6d-a15fa7b9360e)

</div>

## 🔵BallGame2WindowsFormApp
## 📝Описание работы
1. Есть кнопка "Создать", которая создает на форме более 5 шариков, которые начинают двигаться.
2. Необходимо поймать как можно больше шариков с помощью мыши.
3. Считается, что шарик пойман, если он полностью находится на форме.
4. В режиме реального времени пользователю выводится информация о количестве пойманных шариков.

### 🕹️Геймплей
Пример работы приложения приведён ниже:
<div " align="center">

![BallGames2WindowsFormApp](https://github.com/IvanPovaliaev/BallGamesWindowsFormApp/assets/157638990/21e69313-9751-4a9f-aa12-d9decb6070d3)

</div>

## ⚛DiffusionWindowsFormsApp
Программа «Диффузия», моделирующая реальный процесс проникновения молекул одного вещества между молекулами другого.
## 📝Описание работы
1. Экран разделен на две половинки. В каждой части экрана появляется одинаковое количество шариков, но разного цвета.
2. По щелку мыши в произвольном месте шарики начинают случайное движение.
3.  Для каждой границы экрана считается количество соударений со стенкой каждого цвета шариков.
4.  По щелку мыши шарики останавливаются и можно посмотреть "давление газа на стенки сосуда для различных газов".
5.  Когда произойдет "полное перемешивание газов" процесс остановается. Об этом сообщается пользователю и шарики окрашиваются в один цвет
![image](https://github.com/IvanPovaliaev/BallGamesWindowsFormApp/assets/157638990/389339f7-31b1-49ae-8066-f37a09ae9f80)


### 🖥️ Работа приложения
Пример работы приложения приведён ниже:
<div " align="center">

![DiffusionWindowsFormsApp](https://github.com/IvanPovaliaev/BallGamesWindowsFormApp/assets/157638990/890d868a-d741-45de-89c0-9c48f41e1a22)

</div>

## ✨SaluteWinFormsApp
## 📝Описание работы
При нажатии левой кнопки мыши из нижней стенки вылетает один шарик, который в некоторый момент взрывается на много шариков.
### 🕹️Геймплей
Пример работы приложения приведён ниже:
<div " align="center">

![SaluteWinFormsApp](https://github.com/IvanPovaliaev/BallGamesWindowsFormApp/assets/157638990/38d89c78-02ec-4d20-8efb-22996d810483)

</div>

## 🛠️Техническая часть

Проект выполнен с соблюдением принципов `ООП` и использованием `LINQ`.

В решении содержится 5 проектов:
1. Проект общей библиотеки `BallGame.Common`.
2. Проект `BallGameWindowsFormApp`.
3. Проект `BallGame2WindowsFormApp`.
4. Проект `DiffusionWindowsFormsApp`.
5. Проект `SaluteWinFormsApp`

### 🏗️Архитектура

Структура каталога решения:<br />
![image](https://github.com/IvanPovaliaev/BallGamesWindowsFormApp/assets/157638990/19fc68ad-b1c5-4b54-9681-754c803686f1)

`BallGame.Common` является общей библиотекой классов для следующих проектов:
* [BallGamesWindowsFormApp](https://github.com/IvanPovaliaev/BallGamesWindowsFormApp)
* [FruitNinjaWinFormsApp](https://github.com/IvanPovaliaev/FruitNinjaWinFormsApp)
* [AngryBirdsWinFormsApp](https://github.com/IvanPovaliaev/AngryBirdsWinFormsApp)

Библиотека содержит общий класс `Ball`, от которого наследуются остальные шарики.

### 📅Работа с событиями
В классе **BillyardBall** реализована работа с событиями:
```csharp
public class BillyardBall : MoveBall
{
    public event EventHandler<HitEventArgs> OnHited;
    public BillyardBall(Form form) : base(form)
    {
    }
    protected override void Go()
    {
        base.Go();
        if (centerX <= LeftSide())
        {
            vx = -vx;
            OnHited.Invoke(this, new HitEventArgs(Side.Left));
        }
        if (centerX >= RightSide())
        {
            vx = -vx;
            OnHited.Invoke(this, new HitEventArgs(Side.Right));
        }
        if (centerY <= TopSide())
        {
            vy = -vy;
            OnHited.Invoke(this, new HitEventArgs(Side.Top));
        }
        if (centerY >= DownSide())
        {
            vy = -vy;
            OnHited.Invoke(this, new HitEventArgs(Side.Down));
        }
    }
}
```
Когда шарик сталкивается с одной из стенок, то генерируется событие `OnHitted`.
Класс `HitEventArgs`:
```csharp
public class HitEventArgs
{
    public Side Side;
    public HitEventArgs(Side side)
    {
        Side = side;
    }
}
```
