#include <iostream>
#define NOMINMAX
#include <windows.h>
#include "ControladorSemaforo.h"
#include "GerenciadorRede.h"


using namespace std;

int main() {
    // Configura o console para aceitar acentos perfeitamente
    SetConsoleOutputCP(CP_UTF8);
    // 1. Instancia a lógica de negócio (O Semáforo Físico)
    ControladorSemaforo controlador("CRUZAMENTO_01");
    controlador.InicializarSeguranca();

    // 2. Instancia o serviço de infraestrutura (A Rede IoT)

    GerenciadorRede rede(
        controlador,
        "tcp://broker.emqx.io:1883",
        "Semaforo_001",
        "SmartCity/Cruzamento01/Emergencia");

    // 3. Estabelece a conexão com a nuvem
    rede.Conectar();

    cout << "\n=== SISTEMA SCV INTEGRADO E OPERACIONAL ===" << endl;
    cout << "Controle local via teclado: P, T, C, ou Q para sair." << endl;
    cout << "Aguardando disparos de emergência via REDE (Servidor C#)...\n" << endl;

    // 4. Executa o loop infinito do semáforo.
    // As chamadas de rede acontecerão em paralelo de forma transparente!
    controlador.ExecutarCicloNormal();

    // 5. Desconecta ao encerrar (quando você aperta 'Q')
    rede.Desconectar();

    return 0;
}