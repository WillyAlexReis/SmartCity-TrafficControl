# 🧠 Servidor SCV - Orquestração Central

<div align="center">
  <img src="https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#" />
  <img src="https://img.shields.io/badge/.NET_Core-%235C2D91.svg?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET Core" />
  <img src="https://img.shields.io/badge/mqtt-%233C5280.svg?style=for-the-badge&logo=mqtt&logoColor=white" alt="MQTT" />
</div>

<br>

O "cérebro" de validação do ecossistema de tráfego inteligente. 

## 🎯 Objetivo
Este serviço backend atua de forma dupla no ecossistema:
1. **Subscriber:** Fica escutando ininterruptamente as requisições que chegam das viaturas.
2. **Publisher:** Ao receber uma solicitação válida de emergência, ele calcula a rota alvo e publica imediatamente a ordem de intervenção no tópico de hardware do semáforo.

## 🛠️ Tecnologias
* **Ambiente:** .NET Core (Console Application)
* **Linguagem:** C#
* **Mensageria:** Biblioteca MQTTnet
* **Padrão:** Event-Driven Architecture (EDA)

## 🚀 Como Executar
1. Certifique-se de ter o SDK do **.NET Core** instalado.
2. Abra a pasta do projeto no terminal ou a solução no Visual Studio.
3. Se estiver usando o terminal, navegue até a pasta do arquivo principal (`.csproj`) e execute o comando:
   ```bash
   dotnet run
