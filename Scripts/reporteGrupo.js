$(document).ready(function () {
    $("#AddReportBtn").on("click", filterReport);
    $("#datepicker1").kendoDatePicker();
    $("#datepicker2").kendoDatePicker();
});

//report groups methods



function filterReport() {

    var reportModel = GetGroupDate();
    $.ajax({
        type: "Post",
        url: "/Report/GetGroupDate",
        data: JSON.stringify(reportModel),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            showGroupsReportGrid(result);
        },
        error: function (e) { display(e); }
    });
}


function GetGroupDate() {

    var dtpInicial = $("#datepicker1").data("kendoDatePicker");
    var dtpFinal = $("#datepicker2").data("kendoDatePicker");
    var reportModel =
        {
            FechaInicial: dtpInicial.value(),
            FechaFinal: dtpFinal.value()
        };

    return reportModel;
}

function showGroupsReportGrid(result) {

    if (result !== "") {
        createGroupsReportGrid("EstadoGrupoGrid", result);
        $("#EstadoGrupoDiv").show();
    } else
        $("#EstadoGrupoDiv").hide();

}


function createGroupsReportGrid(divId, items) {
    $("#" + divId).kendoGrid({
        dataSource: {
            data: items,
            schema: {
                model: {
                    fields: {
                        IdGrupo: { type: "number" },
                        Nombre: { type: "string" },
                        Descripcion: { type: "string" },
                        FechaCreacion: { type: "date" }
                    }
                }
            },

            pageSize: 5
        },
        //        height: 100,
        scrollable: false,
        sortable: true,
        filterable: true,
        columns: [
            { field: "IdGrupo", title: "Id", width: "30px" },
            { field: "Nombre", title: "Nombre", width: "50px" },
            { field: "Descripcion", title: "Descripcion", width: "50px" },
            { field: "FechaCreacion", title: "Fecha de Creación", width: "50px", format: "{0: dd/MM/yyyy}" },
            {
                width: "40px", attributes: { style: "text-align:center;" }
            }

        ]
    });
}

