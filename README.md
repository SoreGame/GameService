# Основы работы с Unity
Отчет по лабораторной работе #4 выполнил(а):
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
- Используя видео-материалы практических работ 1-5 повторить реализацию приведенного ниже функционала:
        1 Практическая работа «Создание анимации объектов на сцене»
        2 Практическая работа «Создание стартовой сцены и переключение между ними»
        3 Практическая работа «Доработка меню и функционала с остановкой игры»
        4 Практическая работа «Добавление звукового сопровождения в игре»
        5 Практическая работа «Добавление персонажа и сборка сцены для публикации на web-ресурсе»
- Задание 2.
    Привести описание того, как происходит сборка проекта проекта под другие платформы. Какие могут быть особенности?
- Задание 3.
    Добавить в меню Option возможность изменения громкости (от 0 до 100%) фоновой музыки в игре.
- Выводы.
- ✨Magic ✨

## Цель работы
Подготовить разрабатываемое интерактивное приложение к сборке и публикации.

## Задание 1
### Используя видео-материалы практических работ 1-5 повторить реализацию игровых механик

Ход работы:

1) Создал новую сцену _0Scene, разместил объекты на сцене, импортировал новые ассеты 

Результат работы в игре:
![Alt text](img/4/hw1_1.png?raw=true "Title")

Результат работы на сцене:
![Alt text](img/4/hw1_2.png?raw=true "Title")

2) Реализовал анимацию облака в главном меню:
    -Создал контроллер анимации и самуу анимацию через "ключи" в "Animation"

3) Создал кнопки в главном меню. Для их работы написал скрипт "MainMenu"
```csharp
public class MainMenu : MonoBehaviour
{
    public void PlayGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
```  

4) Добавил меню настроек:
![Alt text](img/4/hw1_3.png?raw=true "Title")

Настроил систему переходов в меню. К настройкам и обратно через кнопки "Setting" и "Back"

5) Реализовал механику паузы, создал скрипт "Pause"

```csharp
public class Pause : MonoBehaviour
{
    private bool paused = false;
    public GameObject panel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (!paused)
            {
                Time.timeScale = 0;
                paused = true;
                panel.SetActive(true);
            }
            else 
            {
                Time.timeScale = 1;
                paused = false;
                panel.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
    }
}
```  
Реализация в игре: 
![Alt text](img/4/hw1_5.png?raw=true "Title")

6) Добавил в игру звуковые эффекты ловли яйца и его падения на землю и музыка в меню. 
Для этого изменил скрипты "DragonEgg" и "EnergyShield"

Добалены строки про AudioSource, на примере одного из скриптов:
```csharp
    public AudioSource audioSource;

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

        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
```

7) Добавлена модель мага с анимацией Idle

![Alt text](img/4/hw1_6.png?raw=true "Title")


## Задание 2
###  Привести описание того, как происходит сборка проекта проекта под другие платформы. Какие могут быть особенности?

Под различные платформы необходимо установить дополнительные файлы в юнити
Под различные платформы используются различные SDK и на выходе мы получаем различную структуру проекта и различные исполняемые файлы. 

![Alt text](img/4/hw2_1.png?raw=true "Title")
![Alt text](img/4/hw2_2.png?raw=true "Title")


## Задание 3
### Добавить в меню Option возможность изменения громкости (от 0 до 100%) фоновой музыки в игре:

1) Написан скрипт "SoundSetting"

```csharp
public class SoundSetting : MonoBehaviour
{
    private AudioSource audioSource;
    private float musicVolume = 1f;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = musicVolume;
    }

    public void SetVolume(float vol) 
    {
        musicVolume = vol;
    }
}
```

Также добавлен слайдер в меню настроек:
![Alt text](img/4/hw1_3.png?raw=true "Title")


## Выводы

Подготовил проект к сборке и публикации: удалил неиспользуемые файлы добавленных ассетов, добавлена музыка и моделька мага в игру, также реализовано меню.