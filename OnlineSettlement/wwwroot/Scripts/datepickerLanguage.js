


function SetIdAndlanguage(appId, date_pickerId, language)
{

    var parentDiv = $("#" + appId);
    var datePicker = parentDiv.find("date-picker");
    datePicker.attr("id", date_pickerId);

    datePicker.attr("locale", language);

}


var localeConfigs = {

};

var dateTime;


function SetDefaultDate(_dateTime, date_pickerId) {
    dateTime = _dateTime;
    $('#' + date_pickerId).val(dateTime);
}



function loadVue(appId, SelectedDateId) {
    new Vue({
        el: '#' + appId,

        components: {
            datePicker,

        }, methods: {
            submit(date) {

                dateTime = new Date(date);
                $('#' + SelectedDateId).val(dateTime.toISOString());

                var myElement = document.getElementById(SelectedDateId);
                var event = new Event('change');
                myElement.dispatchEvent(event);

            }

        },

        data() {
            return {
                show: true,
                localeConfigs: localeConfigs

            }

        }
    });

}



