# Основы работы с Unity
Отчет по лабораторной работе #2 выполнил(а):
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
- Используя видео-материалы практических работ 1-5 повторить реализацию игровых механик
- Задание 2.
    Практическая работа «Уменьшение жизни. Добавление текстур».
    Практическая работа «Структурирование исходных файлов в папке».
- Задание 3.
    Практическая работа «Интеграция игровых сервисов в готовое приложение».
- Выводы.
- ✨Magic ✨

## Цель работы
Интеграция интерфейса пользователя в разрабатываемое интерактивное приложение.

## Задание 1
### Используя видео-материалы практических работ 1-5 повторить реализацию игровых механик:
1. Практическая работа «Реализация механизма ловли объектов».
2. Практическая работа «Реализация графического интерфейса с добавлением счетчика очков».


Ход работы:

1) Создаем новый скрипт "EnergyShield" для управления щитом.

```csharp

public class EnergyShield : MonoBehaviour
{
    public TextMeshProUGUI scoreGT;

    private void Start()
    {
        GameObject ScoreGO = GameObject.Find("Score");
        scoreGT = ScoreGO.GetComponent<TextMeshProUGUI>();
        scoreGT.text = "0";
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    private void OnCollisionEnter(Collision coll)
    {
        GameObject Collided = coll.gameObject;
        if (Collided.tag == "Dragon Egg") 
        {
            Destroy(Collided);
        }
        int score = int.Parse(scoreGT.text);
        score += 1;
        scoreGT.text = score.ToString();
    }
}
```

2) Реализуем ловлю объектов "DragonEgg", для этого добавляем в скрипт новый метод.

```csharp
public class DragonEgg : MonoBehaviour
{
    // Start is called before the first frame update
    public static float bottomY = -30f;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = true;

        Renderer rend;
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottomY)
        {
            Destroy(this.gameObject);
            DragonPicker apScript = Camera.main.GetComponent<DragonPicker>();
            apScript.DragonEggDestroy();
        }
    }
}
```
3) Добавляем элемент Canvas и Text, редактируем их под Main Camera для лучшего вида.

![Alt text](img/3/hw1_4.png?raw=true "Title")

4) Создаем счётчик очков, для этого нужно добавить новый метод в скрипт "EnergyShield".

```csharp

public class EnergyShield : MonoBehaviour
{
    public TextMeshProUGUI scoreGT;

    private void Start()
    {
        GameObject ScoreGO = GameObject.Find("Score");
        scoreGT = ScoreGO.GetComponent<TextMeshProUGUI>();
        scoreGT.text = "0";
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    private void OnCollisionEnter(Collision coll)
    {
        GameObject Collided = coll.gameObject;
        if (Collided.tag == "Dragon Egg") 
        {
            Destroy(Collided);
        }
        int score = int.Parse(scoreGT.text);
        score += 1;
        scoreGT.text = score.ToString();
    }
}
```

## Задание 2
### Используя видео-материалы практических работ 1-5 повторить реализацию игровых механик:
3. Практическая работа «Уменьшение жизни. Добавление текстур».
4. Практическая работа «Структурирование исходных файлов в папке».

Ход работы:

1) В скрипте "DragonPicker" добавим условие при котором игра будет перезапускаться.
```csharp

public class DragonPicker : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject energyShieldPrefab;
    public int numEnergyShield = 3;
    public float energyShieldBottomY = -6f;
    public float energyShieldRadius = 1.5f;

    public List<GameObject> shieldList;

    void Start()
    {
        shieldList = new List<GameObject>();
        for (int i = 1; i <= numEnergyShield; i++)
        {
            GameObject tShieldGo = Instantiate<GameObject>(energyShieldPrefab);
            tShieldGo.transform.position = new Vector3(0, energyShieldBottomY, 0);
            tShieldGo.transform.localScale = new Vector3(1*i, 1*i, 1*i);
            shieldList.Add(tShieldGo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DragonEggDestroy() 
    {
        GameObject[] tDragonEggArray = GameObject.FindGameObjectsWithTag("Dragon Egg");
        foreach (GameObject tGO in tDragonEggArray) 
        {
            Destroy(tGO);
        }
        int shieldIndex = shieldList.Count - 1;
        GameObject tShieldGo = shieldList[shieldIndex];
        shieldList.RemoveAt(shieldIndex);
        Destroy(tShieldGo);

        if (shieldList.Count == 0) 
        {
            SceneManager.LoadScene("_0Scene");
        }
    }
}

```

Так же добавляем префаб горы на сцену 
(на скриншотах будет видно)

## Задание 3
### Используя видео-материалы практических работ 1-5 повторить реализацию игровых механик:
5. Практическая работа «Интеграция игровых сервисов в готовое приложение».

1) Импортируем плагин от Яндекс.Игр для корректной инициализации Yandex.SDK
2) Архивируем билд нашей игры в zip-формат и загружаем в Яндекс.Консоль
3) После проверки нашего архива можно перейти на черновик и убедиться, что все работает корректно.


## Выводы

Мы добавили на локацию скайбокс, гору, счетчик..
![Alt text](img/3/hw1_1.png?raw=true "Title")

При ловле счетчик увеличивается на +1
![Alt text](img/3/hw1_2.png?raw=true "Title")

Если не ловить яйца, щиты будут уменьшатся
![Alt text](img/3/hw1_3.png?raw=true "Title")

Интеграция интерфейса пользователя в разрабатываемое интерактивное приложение.

Повторно интегрировал сервисы яндекса в игру, на этот раз с использованием плагина
в интерактивное приложение.
