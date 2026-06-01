# 🧠 Servidor SCV - Orquestração Central

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
