#pragma once
#include <string>

using namespace std;

class ControladorSemaforo {
private:
    string idCruzamento;
    string estadoViaPrincipal;
    string estadoViaTransversal;
    bool modoEmergenciaAtivo;

    void MudarLuzes(string corPrincipal, string corTransversal);

public:
    ControladorSemaforo(string id);
    void InicializarSeguranca();
    void AtivarModoEmergencia(string viaAlvo); // <-- Agora ele pede a direção
    void ExecutarCicloNormal();
};