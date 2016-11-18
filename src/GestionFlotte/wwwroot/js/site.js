// Write your Javascript code.
var marins = [];

jQuery(document).ready(function () {
    var selecteurType = document.getElementById('TypeBateauID');

    // Définition de la méthode de chargement de l'équipage minimum
    // Page en cours = Création / Edition de bateau
    if (selecteurType !== undefined) {
        jQuery(selecteurType).on('change', function () {
            chargementEquipageMinimum(this.value);
        });

        // Premier appel au chargement de la page
        chargementEquipageMinimum(selecteurType.value);

        var roles = loadRoles();

        var additionnalMarin = $('#0')[0];
        if (additionnalMarin !== undefined) {
            listenAddMarin(additionnalMarin);
        }
    }
    
});

function listenAddMarin(elm) {
    
    $($(elm).find('input.add-marin')[0]).on('click', function (event) {
        var marinInput = jQuery(elm).children('input[type="text"]');
        // détection si la saisie est valide. (texte non vide)
        var ok = true;
        for (j = 0; j < marinInput.length; j++) {
            if (marinInput[j].value === '') {
                ok = false;
            }
        }

        if (ok) {
            // création d'un nouvel élément de saisie à partir du model initial
            var newMarin = elm.cloneNode(true);
            // mise à zéro des champs
            $(newMarin).find('input[type="text"').each(function (item) { $(item).val(item.placeholder); });
            // modificationde l'ID
            newMarin.id = parseInt(elm.id) + 1;
            // mise en place de l'écouteur
            listenAddMarin(newMarin);
            // Ajout au DOM
            elm.parentNode.appendChild(newMarin);
            // remplacement du bouton si la saisie est valide.
            $(event.target).replaceWith('<div style="text-align:center; flex-grow: 1;"><i style="color: green;" class ="fa fa-ship fa-2x"></i></div>');
        } else {
            $(event.target).val('Ajouter');
        }
    });
}

// Function de chargement des postes pour le type de bateau
function chargementEquipageMinimum( TypeID ) {
    jQuery.ajax({
        url: "/postes/getPostesType?TypeBateauID=" + TypeID,
        method: "GET",
        dataType: "json"
    })
    .done(function (json) {
        
        var marins = [];
        for (i = 0; i < json.length; i++) {
            var nombreMin = json[i].minimum;

            for (j = 0; j < nombreMin; j++) {        
                var divMarin = `
                    <div class="js-selectable" id="poste_${json[i].role.nom}_req_${j}" style="display:flex;">
                        <input type="text" placeholder="Nom" class ="form-control" style="flex-grow: 1;"/>
                        <input type="text" placeholder="Prenom" class ="form-control" style="flex-grow: 1;"/>
                        <select class="form-control">
                            <option value=${json[i].role.roleID}>${json[i].role.nom}</option>
                        </select>
                        <div style="text-align:center; flex-grow: 1;"><i style="color: red;" class ="fa fa-anchor fa-2x"></i></div>
                    </div>
                `;
                marins.push(divMarin);
            }
        }

        var defaultMarins = jQuery('#membres-minimum h5').siblings();
        for (i = 0; i < defaultMarins.length; i++) {
            defaultMarins[i].remove();
        }

        marins.forEach(function (item) {
            jQuery('#membres-minimum').append(item);
        });

        var domMarins = document.querySelectorAll('.js-selectable');
        for (i = 0; i < domMarins.length; i++) {
            jQuery(domMarins[i]).on('change', function (elm) {
                var marinInput = jQuery(elm.currentTarget).children('input');
                var ok = true;
                for (j = 0; j < marinInput.length; j++) {
                    if (marinInput[j].value === '') {
                        ok = false;
                    }
                }
                if (ok) {
                    $($(elm.currentTarget).find('.fa')[0]).removeClass('fa-anchor').addClass('fa-ship').css('color', 'green');
                }
            });
        }

    })
    .fail(function (xhr, status, errorThrown) {
        alert("Sorry, there was a problem!");
        console.log("Error: " + errorThrown);
        console.log("Status: " + status);
        console.dir(xhr);
    })
    .always(function (xhr, status) {
        //console.log("The request is complete!");
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