$(".escolhaMes").change(function () {
    var mesid = $('.escolhaMes').val();      
       $.ajax({
           url: "Despesas/GastosTotaisdoMes",
        method: "POST",
        data: { mesid: mesid },           
           success: function (dados) {              
            $("canvas#GraficoTotalMes").remove();
            $("div.GraficoTotalMes").append("<canvas id='GraficoTotalMes'style='with: 400px; height: 400px; ' ></canvas>");
            var ctx = document.getElementById("GraficoTotalMes").getContext('2d');
               var grafico = new Chart(ctx, {
                   type: 'pie',
                   data:
                   {
                       labels: ['Restante', 'Total gasto'],
                       datasets: [{
                           label: "Total gasto",
                           backgroundcolor: ["#27ae60", "ac0392b"],
                           data: [(dados.Salario - dados.ValorTotalGasto), dados.ValorTotalGasto]

                       }]
                   },
                   options: {
                       responsive: false,
                       title: {
                           display: true,
                           text: "Total gasto no Mês"

                       }
                   }
               });
            }
    });
});

function CarregarDadosGastosTotaisMes() {
   

    $.ajax({
        url: "Despesas/GastosTotaisdoMes",
        method: "POST",
        data: { mesid: 1 },
        success: function (dados) {
            $("canvas#GraficoTotalMes").remove();
            $("div.GraficoTotalMes").append("<canvas id='GraficoTotalMes'style='with: 400px; height: 400px; ' ></canvas>");
            var ctx = document.getElementById("GraficoTotalMes").getContext('2d');
            var grafico = new Chart(ctx, {
                type: 'pie',
                data:
                {
                    labels: ['Restante', 'Total gasto'],
                    datasets: [
                        {
                            label: "Total gasto",
                            backgroundcolor: ["#27ae60", "ac0392b"],
                            data: [(dados.Salario - dados.ValorTotalGasto), dados.ValorTotalGasto]

                        }
                    ]
                },
                options: {
                    responsive: false,
                    title: {
                        display: true,
                        text: "Total gasto no Mês"  }
                }
            });
        }
    });
   }