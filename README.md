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
Два шара расположены на одтнаковой высоте от "земли", падают с одинаковой скоростью.
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
Два шара расположены на одтнаковой высоте от "земли", падают с одинаковой скоростью.
![Alt text](img/hw2_3.png?raw=true "Title")
Но один из них обладает как бы большей упркгостю и отскакивает выше другого. Сила отскока утихает со временем.
![Alt text](img/hw2_4.png?raw=true "Title")


## Задание 3
### Какова роль параметра Lr? Ответьте на вопрос, приведите пример выполнения кода, который подтверждает ваш ответ. В качестве эксперимента можете изменить значение параметра.

- Перечисленные в этом туториале действия могут быть выполнены запуском на исполнение скрипт-файла, доступного [в репозитории](https://github.com/Den1sovDm1triy/hfss-scripting/blob/main/ScreatingSphereInAEDT.py).
- Для запуска скрипт-файла откройте Ansys Electronics Desktop. Перейдите во вкладку [Automation] - [Run Script] - [Выберите файл с именем ScreatingSphereInAEDT.py из репозитория].

```py

import ScriptEnv
ScriptEnv.Initialize("Ansoft.ElectronicsDesktop")
oDesktop.RestoreWindow()
oProject = oDesktop.NewProject()
oProject.Rename("C:/Users/denisov.dv/Documents/Ansoft/SphereDIffraction.aedt", True)
oProject.InsertDesign("HFSS", "HFSSDesign1", "HFSS Terminal Network", "")
oDesign = oProject.SetActiveDesign("HFSSDesign1")
oEditor = oDesign.SetActiveEditor("3D Modeler")
oEditor.CreateSphere(
	[
		"NAME:SphereParameters",
		"XCenter:="		, "0mm",
		"YCenter:="		, "0mm",
		"ZCenter:="		, "0mm",
		"Radius:="		, "1.0770329614269mm"
	], 
)

```

## Выводы

Абзац умных слов о том, что было сделано и что было узнано.

| Plugin | README |
| ------ | ------ |
| Dropbox | [plugins/dropbox/README.md][PlDb] |
| GitHub | [plugins/github/README.md][PlGh] |
| Google Drive | [plugins/googledrive/README.md][PlGd] |
| OneDrive | [plugins/onedrive/README.md][PlOd] |
| Medium | [plugins/medium/README.md][PlMe] |
| Google Analytics | [plugins/googleanalytics/README.md][PlGa] |

## Powered by

**BigDigital Team: Denisov | Fadeev | Panov**
