$.datepicker.regional['ru'] = {
    closeText: 'Закрыть',
    prevText: 'Пред',
    nextText: 'След',
    currentText: 'Сегодня',
    monthNames: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь',
    'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
    monthNamesShort: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн',
    'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
    dayNames: ['воскресенье', 'понедельник', 'вторник', 'среда', 'четверг', 'пятница', 'суббота'],
    dayNamesShort: ['вск', 'пнд', 'втр', 'срд', 'чтв', 'птн', 'сбт'],
    dayNamesMin: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
    weekHeader: 'Не',
    dateFormat: 'dd.mm.yy',
    firstDay: 1,
    isRTL: false,
    showMonthAfterYear: false,
    yearSuffix: ''
};
$.datepicker.setDefaults($.datepicker.regional['ru']);

$("#datepicker").datepicker({
    onSelect: function (dateText, inst) {
        $(this).css("background-color", "");

        $.ajax({
            type: "POST",
            url: "/Animal/GetAnimals",
            data: { "date": dateText },
            success: function (data) {
             //   alert(data);
                $("#content").html(data);
                $("#date").text(dateText);
            }
        });
    }
});

//CRUD operation
$(document).ready(function () {

    $.ajaxSetup({ cache: false });
    $(document).on("click", ".compItem", function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
           // alert(data);
            $('#dialogContent').html(data);
            $('#modDialog').modal('show');
        });

    });

   
    $("#searchAnimals").change(function (e) {
               e.preventDefault();  
               var text = $("#searchAnimals").val();
               text = text.replace(new RegExp(" ", "g"), "%20");
               alert(text);
               $.ajax({
                   url: "/Animal/ListAnimals",
                   type: "post",
                   cache: false,
                   data:{"text": text},
                   success: function (result)
                   {
                       $("#content").html(result);
                   }
               })
    });

    //pagedList
    $(document).on("click", "#contentPager li[class!=active]  a", function () {
     
        $.ajax({
            url: $(this).attr("href"),
            type: 'get',
            cache: false,
            success: function (result) {
              //  alert(result);
               $('#content').html(result);
            }
        });
        return false;
    });
});