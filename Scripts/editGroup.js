$(document).ready(function () {
    $("#Menu1").kendoMenu();
    $("#AddGroupBtn").on("click", saveGroup);
    $("#cancelAddingGroupBtn").on("click", cancelAddingGroup);
    ////getAllGroups();
});


function loadGroupInfo() {
    //$.ajax({
    //    type: "get",
    //    url: "/Home/GetGroupById?idGrupo=" + idGrupo,
    //    success: function (result) {
    //        var url = "/Home/EditarGrupo";
    //        window.location.href = url;
    //    },
    //    error: function (e) { display(e); }
    //});
}