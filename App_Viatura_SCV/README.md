# 📱 App Tático - Viatura SCV

<div align="center">
  <img src="https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#" />
  <img src="https://img.shields.io/badge/MAUI-%23512BD4.svg?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET MAUI" />
  <img src="https://img.shields.io/badge/xaml-%230C54C2.svg?style=for-the-badge&logo=xaml&logoColor=white" alt="XAML" />
  <img src="https://img.shields.io/badge/mqtt-%233C5280.svg?style=for-the-badge&logo=mqtt&logoColor=white" alt="MQTT" />
  <img src="https://img.shields.io/badge/Visual_Studio_2022-%235C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white" alt="Visual Studio" />
</div>

<br>

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
