$(document).ready(function () {
    var $form = $('form');
    $form.submit(function () {
        $.get($(this).attr('action'), $(this).serialize(), function (response) {
            var results = JSON.parse(response);
            var outtable = document.getElementById("output");
            outtable.rows[1].cells[1].innerHTML = results.CadUsdExh_max;
            outtable.rows[1].cells[2].innerHTML = results.CadUsdExh_min;
            outtable.rows[1].cells[3].innerHTML = results.CadUsdExh_avg;
            outtable.rows[2].cells[1].innerHTML = results.Corra_max;
            outtable.rows[2].cells[2].innerHTML = results.Corra_min;
            outtable.rows[2].cells[3].innerHTML = results.Corra_avg;
            outtable.rows[3].cells[1].innerHTML = results.Pearson;
        }, 'json');
        return false;
    });
});