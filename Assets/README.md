# MVP Shop

## Как запустить
Cцена: Assets\Project\Content\Scenes**Shop** 

## Структура проекта
Все материалы собраны в корневой папке **`Project/`**:

- **Assets/**
  - **README.md** — описание структуры проекта
- **Project/**
  - **Content/** — наращивание контента
    - **Definitions/** — SO-конфиги, баланс
    - **Localization/** — таблицы локализации и конфиг локали
    - **Media/** — арт и аудио-ассеты
    - **Prefabs/** — префабы UI и игровых элементов
    - **Resources/** — только для ProjectContext
    - **Scenes/** — Shop, SingleCard
    - **Settings/** — общие конфиги
  - **Doc/** — документация
    - **UML/** — диаграммы компонентов/модулей
  - **Plugins/** — внешние библиотеки/плагины
    - **\<LibName>/** — Zenject, Mock-ассеты, Editor-интеграции
  - **Src/** — исходный код
    - **Core/** — абстракции, сервисы, утилиты
      - **AssetLoad/** — Addressables/загрузка ресурсов
      - **Command/** — команды и абстракции для ScriptableObject
      - **Definition/** — DI-инсталлеры
      - **Localization/** — Unity Localization имплементация
      - **MVP/** — базовые контроллеры/модели/вью
      - **PlayerData/** — профиль, сохранения, репозитории
      - **SceneTransition/** — прелоадер и ZenjectSceneLoader
    - **Modules/** — домены, зависимые только от Core
    - Флоу у всеx одинаковый, контролер делает прямые вызовы
    - Вглубь модели внешние системы вызывают котролер через ивенты
      - **Gold/**
      - **Health**      
      - **Location/** 
      - **Shop/**    
      - **Vip/**    
