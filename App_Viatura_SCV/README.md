# 📱 App Tático - Viatura SCV

Interface de operação embarcada nos tablets das viaturas de emergência (SAMU, Polícia, Bombeiros). 

## 🎯 Objetivo
Este aplicativo atua como o **Publisher** no ecossistema de mensageria. Ele é operado pelo condutor da viatura para despachar solicitações de preempção de rota (onda verde) para a nuvem de forma instantânea.

## 🛠️ Tecnologias
* **Framework:** .NET 10 MAUI (Multi-platform App UI)
* **Linguagens:** C# e XAML
* **Mensageria:** Biblioteca MQTTnet
* **Broker:** EMQX (Tópico: `SmartCity/Viaturas/Telemetria`)

## 🚀 Como Executar
1. Certifique-se de ter o **Visual Studio 2022** com a carga de trabalho de *Desenvolvimento de interface do usuário de aplicativo multiplataforma do .NET (.NET MAUI)* instalada.
2. Abra a solução `App_Viatura_SCV.slnx` (ou `.sln`).
3. No menu superior de execução, selecione **Windows Machine** (para rodar no desktop) ou um emulador Android.
4. Clique em executar sem depuração (`Ctrl + F5`).
