console.log("✅ AberturaDeCrise.js carregado com sucesso!");

document.addEventListener("DOMContentLoaded", function () {
    // Elementos do formulário
    const textoLivreCheckbox = document.getElementById("textoLivreACN");
    const textoLivreInput = document.getElementById("textoLivreInput");
    const opcoesSelect = document.getElementById("opcoesACN");
    const acnFinal = document.getElementById("acnFinal"); // Campo oculto que será enviado ao banco

    // Verifica se os elementos existem antes de adicionar eventos
    if (!textoLivreCheckbox || !textoLivreInput || !opcoesSelect || !acnFinal) {
        console.error("⚠️ Elementos ACN não encontrados! Verifique os IDs no HTML.");
        return;
    } else {
        alert('teste')
    }

    // Carregar opções no select
    const lista = [
        { value: "", name: "Selecione uma opção" },
        { value: "peoplesoft", name: "Peoplesoft" },
        { value: "datahub", name: "Datahub" },
        { value: "staging", name: "Staging" },
        { value: "profimetrics", name: "Profimetrics" },
        { value: "concentradordpadsp", name: "Concentrador DPA/DSP" },
        { value: "fidelize", name: "Fidelize" },
        { value: "warmup", name: "Warmup" },
        { value: "syncros", name: "Syncros" },
        { value: "aplicacoes", name: "Aplicações" },
        { value: "site", name: "Site" },
        { value: "app", name: "App" },
        { value: "oms", name: "OMS" },
        { value: "retire", name: "Retire" },
        { value: "veroit", name: "VeroIT" },
        { value: "nexp", name: "NEXP" },
        { value: "rappi", name: "RAPPI" },
        { value: "sefaz", name: "SEFAZ" },
        { value: "vtex", name: "VTEX" },
        { value: "ogg", name: "OGG" },
        { value: "freterapido", name: "Frete Rapido" },
        { value: "adyen", name: "Adyen" },
        { value: "wmsdelage", name: "WMS Delage" },
        { value: "wmsknapp", name: "WMS KNAPP" },
        { value: "wmsschaeffer", name: "WMS Schaeffer" },
        { value: "acomp", name: "Acomp" },
        { value: "pbm", name: "PBM" },
        { value: "interplayersportaldadrogaria", name: "Interplayers - Portal da Drogaria" },
        { value: "pactomaisfarmaciapopular", name: "Pacto Mais Farmacia Popular" },
        { value: "clinicarx", name: "ClinicarX" },
        { value: "linxqrlinxagilizaprocfitpos", name: "Linx (QrLinx, Agiliza, Procfit, POS)" },
        { value: "suareceitadigital", name: "Sua receita digital" },
        { value: "trncentre", name: "TrnCentre" },
        { value: "epharma", name: "Epharma" },
        { value: "funcionalcard", name: "Funcional Card" },
        { value: "vidalink", name: "VidaLink" },
        { value: "recargatelecomnet", name: "Recarga TelecomNet" },
        { value: "servicosdepagamento", name: "Serviços de Pagamento" },
        { value: "rede", name: "Rede" },
        { value: "cielo", name: "Cielo" },
        { value: "standby", name: "Standby" },
        { value: "getnet", name: "Getnet" },
        { value: "infraestrutura", name: "Infraestrutura" },
        { value: "firewallswitchlinksintegracao", name: "Firewall, Switch, Links.... Integração" },
        { value: "fw1100e", name: "FW 1100E" },
        { value: "fw1000d", name: "FW 1000D" }
    ];

    function montaLista(array) {
        return array.map(e => `<option value="${e.value}">${e.name}</option>`).join('');
    }

    opcoesSelect.innerHTML = montaLista(lista);

    // Evento ao marcar "Texto Livre"
    textoLivreCheckbox.addEventListener("change", function () {
        const isChecked = textoLivreCheckbox.checked;
        textoLivreInput.disabled = !isChecked;
        opcoesSelect.disabled = isChecked;

        // Limpa o outro campo ao selecionar um
        if (isChecked) {
            opcoesSelect.value = "";
            acnFinal.value = textoLivreInput.value;
        } else {
            textoLivreInput.value = "";
            acnFinal.value = opcoesSelect.value;
        }
    });

    // Atualiza o valor final ao selecionar no select
    opcoesSelect.addEventListener("change", function () {
        if (!textoLivreCheckbox.checked) {
            acnFinal.value = opcoesSelect.value;
        }
    });

    // Atualiza o valor final ao digitar manualmente
    textoLivreInput.addEventListener("input", function () {
        if (textoLivreCheckbox.checked) {
            acnFinal.value = textoLivreInput.value;
        }
    });

    // Validação antes do envio do formulário
    document.querySelector("form").addEventListener("submit", function (event) {
        if (!acnFinal.value) {
            alert("Por favor, selecione ou digite um ACN antes de enviar.");
            event.preventDefault();
        }
    });

    // Define os campos de data e hora com o valor da data e hora atual
    function obterDataHoraAtualFormatada() {
        const agora = new Date();
        const ano = agora.getFullYear();
        const mes = String(agora.getMonth() + 1).padStart(2, '0');
        const dia = String(agora.getDate()).padStart(2, '0');
        const horas = String(agora.getHours()).padStart(2, '0');
        const minutos = String(agora.getMinutes()).padStart(2, '0');
        return `${ano}-${mes}-${dia}T${horas}:${minutos}`;
    }

    // Preencher os campos de data/hora automaticamente
    const dataHoraIncidente = document.getElementById("dataHoraIncidente");
    const dataHoraAcionamento = document.getElementById("dataHoraAcionamento");

    if (dataHoraIncidente && dataHoraAcionamento) {
        dataHoraIncidente.value = obterDataHoraAtualFormatada();
        dataHoraAcionamento.value = obterDataHoraAtualFormatada();
    } else {
        console.error("⚠️ Campos de Data/Hora não encontrados!");
    }
});
