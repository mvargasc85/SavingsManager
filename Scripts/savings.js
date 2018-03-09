$(document).ready(function () {
    $("#Menu1").kendoMenu();
    $("#AddGroupBtn").on("click", saveGroup);
    $("#cancelAddingGroupBtn").on("click", cancelAddingGroup);
    getAllGroups();
});

//groups methods
function saveGroup() {
    
        var groupModel = getGroupModel();
        $.ajax({
            type: "Post",
            url: "/Home/CrearGrupo",
            data: groupModel,
            success: function (result) {
                var url = "/Home/VerGrupos";
                window.location.href = url;
            },
            error: function (e) { display(e); }
        });
}

function getGroupModel() {
    var groupModel =
    {
        IdGrupo: null,
        Nombre: $("#groupNameTxt").val(),
        Descripcion: $("#groupDescriptionTxt").val()
    };
    
    return groupModel;
}

function cancelAddingGroup() {
    $("#groupNameTxt").val("");
    $("#groupDescriptionTxt").val("");
}

function getAllGroups() {
    $.ajax({
        type: "get",
        url: "/Home/GetGrupos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //  async: true,
        success: function(result) {
             showGroupsGrid(result);
        },
        error: function(e) {
             display(e);
        }
    });
}

function showGroupsGrid(result) {

    if (result !== "") {
        createGroupsGrid("groupsGrid", result);
        $("#groupsDiv").show();
        //var url = "/Home/VerGrupos";
        //window.location.href = url;
    } else
        $("#groupsDiv").hide();

}

function createGroupsGrid(divId, items) {
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
            { field: "IdGrupo", title: "Id", width: "50px" },
            { field: "Nombre", title: "Nombre", width: "50px" },
            { field: "Descripcion", title: "Descripcion", width: "50px" },
            { field: "FechaCreacion", title: "Fecha de Creación", width: "50px", format: "{0: dd/MM/yyyy HH.mm.ss}" }
        ]
    });
}


function display(e) { alert(e); }