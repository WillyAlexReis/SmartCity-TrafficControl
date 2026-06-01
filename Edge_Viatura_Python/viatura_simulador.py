import paho.mqtt.client as mqtt
import time
import json 

# Configurações de Rede
BROKER = "broker.hivemq.com"
PORT = 1883
# O tópico onde o Python "grita" e o C# (SCV) escuta
TOPIC_TELEMETRIA = "SmartCity/Viaturas/Telemetria" 

def on_connect(client, userdata, flags, rc):
    print("[SISTEMA] Tablet da Viatura conectado à nuvem SCV com sucesso!\n")

tablet_client = mqtt.Client("Viatura_SAMU_Tablet_01")
tablet_client.on_connect = on_connect

print("=== INICIANDO SISTEMA EMBARCADO (SAMU-192) ===")
print("Conectando antena 4G ao broker HiveMQ...")

tablet_client.connect(BROKER, PORT, 60)
tablet_client.loop_start()

while True:
    print("-" * 50)
    print(">> TELA TOUCHSCREEN - STATUS: PATRULHA (WAZE ABERTO) <<")
    print("1 - BATER NO BOTÃO DA SIRENE (Aproximando via PRINCIPAL)")
    print("2 - BATER NO BOTÃO DA SIRENE (Aproximando via TRANSVERSAL)")
    print("0 - Desligar Viatura")
    
    opcao = input("\nMotorista, selecione a ação no tablet: ")

    # Montamos o pacote de dados (JSON) exatamente como o C# espera ler
    pacote_dados = {
        "id_viatura": "SAMU-192",
        "tipo": "AMBULANCIA",
        "sirene_ligada": False,
        "rota_alvo": ""
    }

    if opcao == "1":
        print("\n[HARDWARE] Sirene ligada! Enviando telemetria para a Central SCV...")
        pacote_dados["sirene_ligada"] = True
        pacote_dados["rota_alvo"] = "PRINCIPAL"
        
        # Dispara o JSON para a nuvem
        tablet_client.publish(TOPIC_TELEMETRIA, json.dumps(pacote_dados))
        time.sleep(1)
        
    elif opcao == "2":
        print("\n[HARDWARE] Sirene ligada! Enviando telemetria para a Central SCV...")
        pacote_dados["sirene_ligada"] = True
        pacote_dados["rota_alvo"] = "TRANSVERSAL"
        
        tablet_client.publish(TOPIC_TELEMETRIA, json.dumps(pacote_dados))
        time.sleep(1)
        
    elif opcao == "0":
        print("Desligando tablet...")
        break
    else:
        print("Botão inválido.")

tablet_client.loop_stop()
tablet_client.disconnect()