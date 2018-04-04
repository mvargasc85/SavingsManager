$(document).ready(function () {
    $("#AddGroupBtn").on("click", saveGroup);
    $("#cancelAddingGroupBtn").on("click", cancelAddingGroup);
    getAllGroups();
});

//groups methods
function saveGroup() {

    var groupModel = getGroupModel();
    $.ajax({
        type: "Post",
        url: "/Group/CrearGrupo",
        data: groupModel,
        success: function (result) {
            var url = "/Group/VerGrupos";
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
        url: "/Group/GetGrupos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //  async: true,
        success: function (result) {
            showGroupsGrid(result);
        },
        error: function (e) {
            display(e);
        }
    });
}



function showGroupsGrid(result) {

    if (result !== "") {
        createGroupsGrid("groupsGrid", result);
        $("#groupsDiv").show();
        //var url = "/Group/VerGrupos";
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
            { field: "Nombre", title: "Nombre", width: "50px" },
            { field: "Descripcion", title: "Descripcion", width: "50px" },
            { field: "FechaCreacion", title: "Fecha de Creación", width: "50px", format: "{0: dd/MM/yyyy}" },
            {
                template: '<a href="javascript:void(0)" class="k-grid-edit" onclick="EditGroup(${IdGrupo})">Editar</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;' +
                    '<a href="javascript:void(0)" class="k-grid-delete" onclick="ConfirmDeleteGroup(${IdGrupo})">Eliminar</a>',
                width: "40px", attributes: { style: "text-align:center;" }
            }

        ]
    });
}


function EditGroup(idGrupo) {
    var url = "/Group/EditarGrupo?idGrupo=" + idGrupo;
    window.location.href = url;
}

function ConfirmDeleteGroup(idGrupo) {
    $("#delete-group-dialog-confirm").dialog({
        resizable: false,
        height: "auto",
        width: 400,
        modal: true,
        buttons: {
            "Eliminar": function () {
                deleteGroup(idGrupo);
            },
            "Cancelar": function () {
                $(this).dialog("close");
            }
        }
    });
}

function deleteGroup(idGrupo) {
    $.ajax({
        type: "Post",
        url: "/Group/EliminarGrupo?idGrupo=" + idGrupo,
        success: function (result) {
            var url = "/Group/VerGrupos";
            window.location.href = url;
        },
        error: function (e) { display(e); }
    });
}

function updateGroup() {

    var groupModel = getGroupModel();
    $.ajax({
        type: "Post",
        url: "/Group/CrearGrupo",
        data: groupModel,
        success: function (result) {
            var url = "/Group/VerGrupos";
            window.location.href = url;
        },
        error: function (e) { display(e); }
    });
}