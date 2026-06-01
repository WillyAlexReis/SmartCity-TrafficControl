#pragma once
#define PAHO_MQTTPP_IMPORTS // <-- A CHAVE MÁGICA PARA O LINKER DO WINDOWS
#include <string>
#include "mqtt/async_client.h"
#include "ControladorSemaforo.h"

using namespace std;

// A classe de Callback (A "Secretária") agora é declarada aqui.
// Ela herda das funcionalidades do MQTT e precisa conhecer o ControladorSemaforo.
class MqttCallback : public virtual mqtt::callback {
private:
    ControladorSemaforo& _controlador;

public:
    MqttCallback(ControladorSemaforo& controlador);
    void connection_lost(const string& cause) override;
    void delivery_complete(mqtt::delivery_token_ptr token) override;
    void message_arrived(mqtt::const_message_ptr msg) override;
};

// A classe principal que envelopa a complexidade do MQTT
class GerenciadorRede {
private:
    string _serverAddress;
    string _clientId;
    string _topic;

    mqtt::async_client _client;
    MqttCallback _cb;

public:
    GerenciadorRede(ControladorSemaforo& controlador, string endereco, string idCliente, string topicoAlvo);
    void Conectar();
    void Desconectar();
};