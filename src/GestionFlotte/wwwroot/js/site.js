// Write your Javascript code.
jQuery(document).ready(function () {
    var selecteurType = document.getElementById('TypeBateauID');

    var roles = loadRoles();

    // Définition de la méthode de chargement de l'équipage minimum
    jQuery(selecteurType).on('change', function () {
        chargementEquipageMinimum(this.value);
    });
    // Premier appel au chargement de la page
    chargementEquipageMinimum(selecteurType.value);
});

// Function de chargement des postes pour le type de bateau
function chargementEquipageMinimum( TypeID ) {
    jQuery.ajax({
        url: "/postes/getPostesType?TypeBateauID=" + TypeID,
        method: "GET",
        dataType: "json"
    })
    .done(function (json) {
        //console.log(json)
    })
    .fail(function (xhr, status, errorThrown) {
        alert("Sorry, there was a problem!");
        console.log("Error: " + errorThrown);
        console.log("Status: " + status);
        console.dir(xhr);
    })
    .always(function (xhr, status) {
        console.log("The request is complete!");
    });
}

function loadRoles() {
    jQuery.ajax({
        url: "/roles/json",
        method: "GET",
        dataType: "json"
    })
    .done(function (json) {
        //console.log(json)
    })
    .fail(function (xhr, status, errorThrown) {
        alert("Sorry, there was a problem!");
        console.log("Error: " + errorThrown);
        console.log("Status: " + status);
        console.dir(xhr);
    })
    .always(function (xhr, status) {
        console.log("The request is complete!");
    });
}