#include "ControladorSemaforo.h"
#include <iostream>
#include <thread>
#include <chrono>
#include <conio.h>

using namespace std;

ControladorSemaforo::ControladorSemaforo(string id) {
    idCruzamento = id;
    modoEmergenciaAtivo = false;
    estadoViaPrincipal = "DESLIGADO";
    estadoViaTransversal = "DESLIGADO";
}

void ControladorSemaforo::MudarLuzes(string corPrincipal, string corTransversal) {
    estadoViaPrincipal = corPrincipal;
    estadoViaTransversal = corTransversal;
    cout << "[" << idCruzamento << "] PRINCIPAL: " << estadoViaPrincipal
        << " | TRANSVERSAL: " << estadoViaTransversal << endl;
}

void ControladorSemaforo::InicializarSeguranca() {
    cout << "\n[SISTEMA] Iniciando boot do controlador..." << endl;
    for (int i = 0; i < 3; i++) {
        MudarLuzes("AMARELO_PISCANTE", "AMARELO_PISCANTE");
        this_thread::sleep_for(chrono::seconds(1));
        MudarLuzes("DESLIGADO", "DESLIGADO");
        this_thread::sleep_for(chrono::seconds(1));
    }
    cout << "[SISTEMA] Boot concluido. Iniciando trafego normal.\n" << endl;
}

// O método agora processa a direção exata da emergência
void ControladorSemaforo::AtivarModoEmergencia(string viaAlvo) {
    modoEmergenciaAtivo = true;
    cout << "\n=================================================" << endl;
    cout << "[ALERTA] Preempcao ativada. Alvo: " << viaAlvo << endl;

    // Força o amarelo para quem quer que esteja no verde frear
    if (estadoViaPrincipal == "VERDE" || estadoViaTransversal == "VERDE") {
        MudarLuzes(
            estadoViaPrincipal == "VERDE" ? "AMARELO" : "VERMELHO",
            estadoViaTransversal == "VERDE" ? "AMARELO" : "VERMELHO"
        );
        this_thread::sleep_for(chrono::seconds(3));
    }

    // 1 segundo de segurança com cruzamento totalmente fechado
    MudarLuzes("VERMELHO", "VERMELHO");
    this_thread::sleep_for(chrono::seconds(1));

    // Toma a decisão baseada na via que a viatura está vindo
    if (viaAlvo == "PRINCIPAL") {
        MudarLuzes("VERDE (EMERGENCIA)", "VERMELHO");
        cout << "Via Principal liberada para a viatura." << endl;
    }
    else if (viaAlvo == "TRANSVERSAL") {
        MudarLuzes("VERMELHO", "VERDE (EMERGENCIA)");
        cout << "Via Transversal liberada para a viatura." << endl;
    }
    else if (viaAlvo == "CONTRA_MAO") {
        MudarLuzes("VERMELHO (BLOQUEIO)", "VERMELHO (BLOQUEIO)");
        cout << "[CRITICO] Viatura na contramao! Cruzamento totalmente bloqueado." << endl;
    }

    cout << "=================================================\n" << endl;

    this_thread::sleep_for(chrono::seconds(5)); // Tempo para a viatura passar

    cout << "\n[SISTEMA] Viatura cruzou. Retornando ao controle normal..." << endl;
    modoEmergenciaAtivo = false;
    InicializarSeguranca();
}

void ControladorSemaforo::ExecutarCicloNormal() {
    int faseAtual = 0;
    int contadorTempo = 0;

    while (true) {
        // Modificado para aceitar teclas diferentes
        if (_kbhit()) {
            char tecla = _getch();
            tecla = toupper(tecla);

            if (tecla == 'P') {
                AtivarModoEmergencia("PRINCIPAL");
                faseAtual = 0; contadorTempo = 0; continue;
            }
            else if (tecla == 'T') {
                AtivarModoEmergencia("TRANSVERSAL");
                faseAtual = 0; contadorTempo = 0; continue;
            }
            else if (tecla == 'C') {
                AtivarModoEmergencia("CONTRA_MAO");
                faseAtual = 0; contadorTempo = 0; continue;
            }
            else if (tecla == 'Q') {
                cout << "Encerrando sistema..." << endl;
                break;
            }
        }

        if (!modoEmergenciaAtivo) {
            if (faseAtual == 0) {
                if (contadorTempo == 0) MudarLuzes("VERDE", "VERMELHO");
                if (contadorTempo >= 10) { faseAtual = 1; contadorTempo = -1; }
            }
            else if (faseAtual == 1) {
                if (contadorTempo == 0) MudarLuzes("AMARELO", "VERMELHO");
                if (contadorTempo >= 3) { faseAtual = 2; contadorTempo = -1; }
            }
            else if (faseAtual == 2) {
                if (contadorTempo == 0) MudarLuzes("VERMELHO", "VERDE");
                if (contadorTempo >= 10) { faseAtual = 3; contadorTempo = -1; }
            }
            else if (faseAtual == 3) {
                if (contadorTempo == 0) MudarLuzes("VERMELHO", "AMARELO");
                if (contadorTempo >= 3) { faseAtual = 0; contadorTempo = -1; }
            }

            contadorTempo++;
            this_thread::sleep_for(chrono::seconds(1));
        }
    }
}