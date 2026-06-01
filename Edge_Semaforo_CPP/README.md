# 🚦 Controlador Edge - Semáforo C++

Software embarcado de baixo nível responsável pelo acionamento do hardware físico nos cruzamentos.

## 🎯 Objetivo
Este sistema atua como o **Subscriber e Atuador**. Ele é projetado para rodar em dispositivos Edge (como Raspberry Pi ou Controladores Lógicos Programáveis - CLPs) diretamente nas caixas de controle dos semáforos. Ele escuta as ordens de emergência do Servidor SCV e realiza o chaveamento de estados do ciclo de trânsito (Verde/Amarelo/Vermelho e amarelo piscante de emergência) em milissegundos.

## 🛠️ Tecnologias
* **Linguagem:** C++ Nativo (Padrão C++11 ou superior)
* **Mensageria:** Biblioteca Eclipse Paho MQTT (Paho MqttCpp)
* **Arquitetura:** Injeção de Dependência (Separando regras de hardware das regras de rede)

## 🚀 Como Executar
1. Certifique-se de ter o **Visual Studio** com a carga de trabalho *Desenvolvimento para Desktop com C++* instalada.
2. É necessário ter a biblioteca `paho.mqtt.cpp` devidamente configurada no *Linker* e *Include Directories* do projeto.
3. Defina este projeto como o principal na solução.
4. Compile e execute sem depuração (`Ctrl + F5`).
5. O console exibirá o boot do controlador e aguardará a interrupção via rede.
