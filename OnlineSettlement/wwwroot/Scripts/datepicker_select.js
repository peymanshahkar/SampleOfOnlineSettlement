

    var dateFrom;
    var dateTo;

 function GetDateFrom(){
        return dateFrom;
}

    function GetDateTo() {
        return dateTo;
    }

function SetDefaultDate(from,to){
        dateFrom = from;
        dateTo = to;

      $('#dateRange').val(from + ' - ' + to);
  }


    $(function () {

        new Vue({
            el: '#app',
            components: {
                datePicker,

            }, methods: {
                submit(date) {

                     dateFrom = new Date(date[0]);
                     dateTo = new Date(date[1]);
                }
            },

            data() {
                return {
                    show: true,
                    localeConfigs: {
                        en: {

                            translations: {
                                submit: "تایید",
                            }

                        }
                    }
                }

            }
        });

    });
