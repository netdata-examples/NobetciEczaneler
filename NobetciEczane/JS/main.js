$(document).ready(function () {
    $('.selectpicker').selectpicker({
        style: 'btn-default',
        liveSearchStyle: 'startsWith',
        width: '100%',
        size: '10'
    });

    $("#txtIl").on('changed.bs.select', function (e) {
        var il = $("#txtIl").val();
        if (il == "0") {
            $("#txtIlce").empty();
            $("#txtIlce").html("<option>İlçe Seçiniz...</option>").selectpicker('refresh');
        }
        else {
            IlceleriYukle(il);
        }
    });

    IlleriYukle();
});


function IlleriYukle() {
    $.ajax({
        type: "POST",
        url: "/eczaneler.aspx/IlleriYukle",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: '{}',
        async: true,
        success: function (result) {
            var sonuc = JSON.parse(result.d)
            if (sonuc.Hata == "") {
                $("#txtIl").empty();
                $("#sagTarafIller").empty();
                $("#txtIl").append(sonuc.Iller).selectpicker('render');
                var il = $("#hfSehir").val();
                $("#txtIl").val(il).selectpicker('refresh');
                IlceleriYukle(il)

                $("#sagTarafIller").append(sonuc.SagTarafIller);
            }
            else {
                $("#txtIl").empty();
                bootbox.alert(sonuc.Hata);
            }
        },
        error: function (xhr, status, error) {
            bootbox.alert(error);
        }
    });
}

function IlceleriYukle(il) {
    $.ajax({
        type: "POST",
        url: "/eczaneler.aspx/IlceleriYukle",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: '{"il":"' + il + '"}',
        async: true,
        success: function (result) {
            var sonuc = JSON.parse(result.d)
            if (sonuc.Hata == "") {
                $("#txtIlce").empty();
                $("#txtIlce").append(sonuc.Ilceler).selectpicker('refresh');
                var ilce = $("#hfIlce").val();
                if (ilce != "") {
                    $("#txtIlce").val(ilce).selectpicker("refresh");
                }
            }
            else {
                bootbox.alert(sonuc.Hata);
            }
        },
        error: function (xhr, status, error) {
            bootbox.alert(error);
        }
    });
}

function EczaneAra() {
    var il = $('#txtIl option:selected').text();
    var ilce = $("#txtIlce").val().trim();
    $.ajax({
        type: "POST",
        url: "/eczaneler.aspx/AramaUrlDon",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{il:" + JSON.stringify(il) + ",ilce:" + JSON.stringify(ilce) + "}",
        async: true,
        success: function (result) {
            if (result.d == "0") {
                bootbox.alert("Lütfen il seçiniz!");
            } else {
                window.location.href = result.d;
            }
        },
        error: function (xhr, status, error) {
            bootbox.alert(error);
        }
    });
}
