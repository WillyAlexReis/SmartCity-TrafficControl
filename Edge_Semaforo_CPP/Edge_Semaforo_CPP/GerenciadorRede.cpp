#include "GerenciadorRede.h"
#include <iostream>

using namespace std;

// ==========================================================
// IMPLEMENTAÇÃO DO CALLBACK (A "Secretária")
// ==========================================================

MqttCallback::MqttCallback(ControladorSemaforo& controlador) : _controlador(controlador) {}

void MqttCallback::connection_lost(const string& cause) {
    cout << "\n[REDE] Conexão com o Broker perdida!" << endl;
}

void MqttCallback::delivery_complete(mqtt::delivery_token_ptr token) {}

void MqttCallback::message_arrived(mqtt::const_message_ptr msg) {
    string comandoVia = msg->to_string();

    cout << "\n===================================================" << endl;
    cout << ">> [SINAL DE REDE MQTT RECEBIDO] <<" << endl;
    cout << "Ordem de preempção para a via: " << comandoVia << endl;
    cout << "===================================================" << endl;

    // Injeção de dependência na prática: repassamos a ordem para a inteligência física
    _controlador.AtivarModoEmergencia(comandoVia);
}

// ==========================================================
// IMPLEMENTAÇÃO DO GERENCIADOR DE REDE
// ==========================================================

GerenciadorRede::GerenciadorRede(ControladorSemaforo& controlador, string endereco, string idCliente, string topicoAlvo)
    : _serverAddress(endereco),
    _clientId(idCliente),
    _topic(topicoAlvo),
    _client(endereco, idCliente),
    _cb(controlador)
{
    _client.set_callback(_cb); // Amarra a "escuta" ao cliente
}

void GerenciadorRede::Conectar() {
    mqtt::connect_options connOpts;
    connOpts.set_clean_session(true);

    try {
        cout << "[REDE] Conectando ao Broker publico EMQX..." << endl;
        _client.connect(connOpts)->wait();
        cout << "[REDE] Conectado! Ativando escuta de viaturas..." << endl;

        // Assina o tópico e começa a monitorar em segundo plano
        _client.subscribe(_topic, 1)->wait();
    }
    catch (const mqtt::exception& exc) {
        cerr << "\n[ERRO CRÍTICO] Falha na integração IoT: " << exc.what() << endl;
    }
}

void GerenciadorRede::Desconectar() {
    try {
        cout << "[SISTEMA] Desconectando da rede com segurança..." << endl;
        _client.disconnect()->wait();
    }
    catch (const mqtt::exception& exc) {
        cerr << "\n[ERRO] Falha ao desconectar: " << exc.what() << endl;
    }
}