# 🐍 Simulador de Viatura - Python (Mock Client)

Script leve de simulação de hardware para testes de integração (Stubs) e validação de tráfego de rede, sem a necessidade de inicializar o framework pesado do aplicativo tático.

## 🎯 Objetivo
Este módulo não é o aplicativo de produção embarcado na viatura, mas sim uma ferramenta de engenharia e testes. Ele simula o envio do JSON de telemetria da viatura para a nuvem, permitindo que a equipe valide a resposta do Servidor Central (C#) e o acionamento do Semáforo (C++) de forma rápida pelo terminal.

## 🛠️ Tecnologias
* **Linguagem:** Python 3
* **Mensageria:** Biblioteca `paho-mqtt`
* **Formato de Dados:** JSON (Serialização de dicionários)

## 🔄 Histórico de Arquitetura (Aviso de Broker)
Este script foi utilizado na **Fase 1 (PoC)** do projeto, quando a comunicação estava roteada através do broker público de testes `broker.hivemq.com`. 

Na **Fase 2 (Atual)**, o ecossistema foi migrado para o broker `EMQX`. Para reutilizar este simulador nas arquiteturas mais recentes, atualize a variável global de configuração no início do código:
* **De:** `BROKER = "broker.hivemq.com"`
* **Para:** `BROKER = "[O IP DO SEU BROKER EMQX ATUAL]"`

## 🚀 Como Executar
1. Certifique-se de ter o interpretador **Python 3** instalado na sua máquina.
2. Instale a biblioteca MQTT via terminal:
   ```bash
   pip install paho-mqtt
