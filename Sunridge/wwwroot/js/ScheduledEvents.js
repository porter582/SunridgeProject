var selectedEvent = null;
$(document).ready(function () {
    var events = [];
    $.ajax({
        type: "GET",
        url: "/api/Events",
        success: function (data) {
            $.each(data, function (i, v) {
                events.push({
                    id: v.id,
                    title: v.subject,
                    description: v.description,
                    location: v.location,
                    image: v.image,
                    start: moment(v.start),
                    end: v.end != null ? moment(v.end) : null,
                    allDay: v.isFullDay
                });
            });
            generateCalendar(events);
        },
        error: function (error) {
            alert('failed');
        }
    });
    function generateCalendar(events) {
        // When generating cal, destroy whatever already exists
        $('#calendar').fullCalendar('destroy');
        // Calendar settings
        $('#calendar').fullCalendar({
            contentHeight: 400,
            defaultDate: new Date(),
            timeFormat: 'h(:mm)a',
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,listYear'
            },
            eventLimit: true,
            eventTextColor: 'white',
            events: events,
            eventClick: function (calEvent, jsEvent, view) {
                selectedEvent = calEvent.id;
                $('#calendarEventModal #eventTitle').text(calEvent.title);
                var $description = $('<div/>');
                if (calEvent.image != null) {
                    $description.append($('<p/>').html('<img src="' + calEvent.image + '" class="img-responsive">'));
                }
                $description.append($('<p/>').html('<b>Description: </b>' + calEvent.description));
                if (calEvent.location != null) {
                    $description.append($('<p/>')
                        .html('<b>Location: </b>' + calEvent.location));
                }
                $description.append($('<p/>')
                    .html('<b>Start: </b>' + calEvent.start.format("dddd, MMMM Do YYYY, h:mm:ss a")));
                if (calEvent.end != null) {
                    $description.append($('<p/>')
                        .html('<b>End: </b>' + calEvent.end.format("dddd, MMMM Do YYYY, h:mm:ss a")));
                }
                $('#calendarEventModal #eventDetails').empty().html($description);
                $('#calendarEventModal').modal();
            }
        });
    }
})
function editEvent() {
    window.location.href = '/Pages/Admin/ScheduledEvents/Edit/' + selectedEvent;
}