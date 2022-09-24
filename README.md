# Основы работы с Unity
Отчет по лабораторной работе #1 выполнил(а):
- Соломеин Егор Александрович 
- РИ300013
Отметка о выполнении заданий (заполняется студентом):

| Задание | Выполнение | Баллы |
| ------ | ------ | ------ |
| Задание 1 | * | 60 |
| Задание 2 | * | 20 |
| Задание 3 | * | 20 |

знак "*" - задание выполнено; знак "#" - задание не выполнено;

Работу проверили:
- к.т.н., доцент Денисов Д.В.
- к.э.н., доцент Панов М.А.
- ст. преп., Фадеев В.О.

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Структура отчета

- Задание 1.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 2.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 3.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Выводы.
- ✨Magic ✨

## Цель работы
ознакомиться с основными функциями Unity и взаимодействием с объектами внутри редактора.

## Задание 1
### Пошагово выполнить каждый пункт раздела "ход работы" с описанием и примерами реализации задач
Ход работы:

1) Создать новый проект из шаблона 3D – Core;
2) Проверить, что настроена интеграция редактора Unity и Visual Studio Code
(пункты 8-10 введения);
3) Создать объект Plane;
4) Создать объект Cube;
5) Создать объект Sphere;
6) Установить компонент Sphere Collider для объекта Sphere;
7) Настроить Sphere Collider в роли триггера;
8) Объект куб перекрасить в красный цвет;
9) Добавить кубу симуляцию физики, при это куб не должен проваливаться
под Plane;
10) Написать скрипт, который будет выводить в консоль сообщение о том,
что объект Sphere столкнулся с объектом Cube;
11) При столкновении Cube должен менять свой цвет на зелёный, а при
завершении столкновения обратно на красный.

Сцена начинается вот с такого расположения объектов на сцене
![Alt text](img/hw1_1.png?raw=true "Title")
При касании сферы с кубом, материал куба менят цвет на зеленый
![Alt text](img/hw1_2.png?raw=true "Title")
После падения куба на землю, сфера взрывается и разлетается на маленькие сферы
![Alt text](img/hw1_3.png?raw=true "Title")


Код для взрывва сферы (DestroyObject.cs): 

```csharp

public class DestroyObject : MonoBehaviour
{
    public float radius = 5.0f;
    public float force = 10.0f;

    public GameObject prefabBoomPoint;
    public GameObject prefabSphere;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Icosphere")
        {
            Destroy(collision.gameObject);
            Vector3 boomPosition = collision.gameObject.transform.position;
            Instantiate(prefabBoomPoint, collision.transform.position, collision.transform.rotation);
            Instantiate(prefabSphere, collision.transform.position, collision.transform.rotation);
            Collider[] colliders = Physics.OverlapSphere(boomPosition, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, boomPosition, 3.0f);
                }
            }
        }
    }
}

```

## Задание 2
### Продемонстрируйте на сцене в Unity следующее:
#### Что произойдёт с координатами объекта, если он перестанет быть дочерним?

Это координаты объекта до того как он стал дочерним
![Alt text](img/hw2_1.png?raw=true "Title")

После того как он стал дочерним, его координаты изменились. На координаты относительто родительского объекта. Как бы берут точку начала отсчета от позиции родительского объекта.
![Alt text](img/hw2_2.png?raw=true "Title")

#### Создайте три различных примера работы компонента RigidBody?

##### Пример 2
Два шара расположены на одинаковой высоте от "земли", падают с одинаковой скоростью.
![Alt text](img/hw2_3.png?raw=true "Title")
Но один из них обладает как бы большей упркгостю и отскакивает выше другого. Сила отскока утихает со временем.
![Alt text](img/hw2_4.png?raw=true "Title")

Код отскока (только) от горизонтальной поверхкности (RepulsWall.cs)

```csharp
public class RepulsWall : MonoBehaviour
{
	Rigidbody rb;
	public float repulceForece;
	void Start()
	{
		if (rb == null)
		{
			rb = GetComponent<Rigidbody>();
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Wall")
		{
			Debug.Log(rb.velocity);
			rb.velocity = new Vector3(0, collision.relativeVelocity.y*repulceForece*0.1f, 0);
			Debug.Log("Posle " +rb.velocity);
		}
	}
}
```

##### Пример 3
Стартовое положение объектов на сцене
![Alt text](img/hw2_5.png?raw=true "Title")

Полсе того как куб упадет на землю, стена мешающая движению шара исчезнет
![Alt text](img/hw2_6.png?raw=true "Title")

После того как шар попадет на цветную платформу он начнет отскакивать от нее.
![Alt text](img/hw2_7.png?raw=true "Title")

Каждое касание шара с платформой делает платформу все более и более красной.
![Alt text](img/hw2_8.png?raw=true "Title")


Код для исчезновения стены (DeleteWall.cs)
```csharp
public class DeleteWall : MonoBehaviour
{

    public GameObject deleteWall;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sphere") 
        {
            Destroy(deleteWall);
        }
    }
}
```

Код изменения цвета платформы (collorChange.cs.)
```csharp
public class collorChange : MonoBehaviour
{

    public float value;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sphere") 
        {
            this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.Lerp(this.GetComponent<Renderer>().materials[0].color, Color.red, value));
        }
    }
}

```

## Задание 3
### Реализуйте на сцене генерацию n кубиков. Число n вводится пользователем после старта сцены.

После старта сцены в левом нижнем углу существует поле для ввода числогого значения 
![Alt text](img/hw3_1.png?raw=true "Title")

После окончания ввода на сцене появляются объекты (кубы) с некоторой задержкой, которую можно установить в инспекторе 
![Alt text](img/hw3_2.png?raw=true "Title")

Код для генерации обьектов на сцене (ObjectSpawner.cs)

```csharp
public class ObjectSpawner : MonoBehaviour
{
    public string cubeCount;
    public float pauseTime;
    public GameObject spawnObject;
    public GameObject inputField;

    // Start is called before the first frame update
    void Start()
    {
        waiter();
    }

    public async void waiter()
    {
        cubeCount = inputField.GetComponent<Text>().text;

        for (int i = 0; i < int.Parse(cubeCount); i++)
        {
            Instantiate(spawnObject, this.transform.position, this.transform.rotation);
            await Task.Delay(TimeSpan.FromSeconds(pauseTime));
        }
    }
}
```
## Выводы

Ознакомился с основными функциями Unity и взаимодействием с объектами внутри редактора.
Создал примеры взаимодействия RigitBody на сцене
Реализовал генерацию обьектов после старта сцены, освовываясь на введеных "игроком" данных   
