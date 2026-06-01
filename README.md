# 🚑 SmartCity-TrafficControl

Plataforma IoT e Edge Computing para priorização de veículos de emergência e controlo semafórico em tempo real.

---

## 📖 O Projeto
Este ecossistema distribuído foi construído para resolver um problema crítico de trânsito em polos urbanos: o tempo de resposta de veículos de emergência (SAMU, Bombeiros, Polícia). O sistema cria uma "onda verde" inteligente, permitindo que a viatura comunique de forma assíncrona com os controladores semafóricos da cidade via nuvem, interrompendo o tráfego transversal e libertando a via principal com segurança.

## 🏗️ Arquitetura do Sistema
A solução utiliza o padrão de mensageria **Publish/Subscribe** para garantir o desacoplamento dos serviços, tolerância a falhas e baixíssima latência na transmissão de dados.

* **Aplicativo Tático (Publisher):** Interface embarcada na viatura que envia a solicitação de preempção de rota.
* **Broker MQTT (Middleman):** Servidor corporativo em nuvem (EMQX) responsável por rotear as mensagens instantaneamente.
* **Servidor SCV (Subscriber/Validador):** O "cérebro" central que processa regras de negócio e autoriza a intervenção na via.
* **Semaforo Edge (Subscriber/Atuador):** Controlador que executa a troca de luzes em milissegundos através de escuta de rede de baixo nível.

## 🚀 Tecnologias Utilizadas
* **Front-End Embarcado:** .NET 10 MAUI (Multi-platform App UI) com XAML e C#
* **Back-End Central:** .NET Core C#
* **Edge Computing:** C++ Nativo (com biblioteca Eclipse Paho)
* **Comunicação IoT:** Protocolo MQTT e Servidor EMQX
* **Padrões de Projeto:** Injeção de Dependência e Design Baseado a Eventos (EDA)

## 📂 Estrutura do Repositório
O projeto está organizado no padrão Monorepo, separando as responsabilidades de cada componente independente:

* `App_Viatura_SCV/`: Código-fonte do aplicativo tático multiplataforma da viatura. Consulte o README interno desta pasta para instruções de build.
* `Servidor_SCV_CSharp/`: Lógica central de validação e roteamento. Consulte o README interno desta pasta para inicialização.
* `Edge_Semaforo_CPP/`: Código do atuador do hardware do semáforo. Consulte o README interno desta pasta para compilação.

## 🗺️ Roadmap e Próximos Passos
A plataforma está em evolução contínua para simular ambientes de produção cada vez mais reais. As próximas atualizações focam em:

* Implementação de comunicação segura com certificados TLS/SSL no broker MQTT.
* Integração de rastreamento geográfico (GPS) em tempo real na interface tática.
* Expansão para acionamento de hardware físico (relés e GPIO).
* Construção de um Dashboard Web para a central de monitorização de tráfego.

---
**Desenvolvido por Williadson Sales** - Analista e Desenvolvedor de Sistemas.
