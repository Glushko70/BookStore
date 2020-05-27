$(document).ready(function () {
    var formsMap = new Object();
    var numberNewForm = 0;

    $('.delete-book').on('click', function () {
        deleteBook(this);
    });

    $('#add-book').on('click', function (e) {
        e.preventDefault();

        $.get(this.href, function (data) {
            $('#modalContent').html(data);
            $('#modalDialogAdd').modal('show');
            $('.save-book').on('click', function (e) {
                saveBook(e);
            });
        })
    });

    function saveBook(e) {
        e.preventDefault();
        var form = $('#modalDialogAdd form');
        var book = form.serializeArray();        
        
        if (showErrors(book)) {
            formsMap[numberNewForm] = form;                        
            $('#modalDialogAdd').modal('hide');
            book['categoryName'] = $('#BookCategoryId option:selected').text();
            insertTableRow(book);
            $('.delete-book').unbind();
            $('.delete-book').on('click', function () {
                deleteBook(this);
            });            
        }
    }

    function showErrors(book) {
        var isValid = true;
        $.each(book, function (_, kv) {
            var id = '#' + kv.name + '_validationMessage';
            if ($.trim(kv.value).length == 0) {
                if (id == "#BookCategoryId_validationMessage" || id == '#Athor_validationMessage' || id == "#Name_validationMessage")
                    isValid = false;
                $(id).show();
            } else {
                $(id).hide();
            }
        });
        return isValid;
    }

    function deleteBook(el) { 
        var id = $(el).data('id');
        if (id && id > 0) {
            $.post("Home/Delete", { id : id }, function (data) {
                if (data.key == true) {
                    var element = $(el).parent().parent();
                    var currentIndexForm = element.data('formnumber');
                    delete formsMap[currentIndexForm];
                    element.remove();
                }
            });
        } else {
            var element = $(el).parent().parent();
            var currentIndexForm = element.data('formnumber');
            delete formsMap[currentIndexForm];
            element.remove();
        }       
        
    }

    function insertTableRow(book) {

        $('#table-body').append("<tr data-formNumber='" + numberNewForm++ + "'><td class='td-book-id'>" + 0 + "</td><td>"
            + book['categoryName'] + "</td><td>" + book[2].value + "</td><td>"
            + book[3].value + "</td><td>" + book[4].value + "</td><td><span class='delete-book btn btn-danger btn-sm'>Видалити</span></td></tr>");
    }

    var interval = setInterval(sendDataToServer, 30000);

    function sendDataToServer() {
        $.each(formsMap, function (key, value) {
            var form = value;
            $.post(form.attr('action'), form.serializeArray(), function (data) {
                if (data.key == true) {
                    delete formsMap[key];
                    var row = $('#table-body').find("[data-formNumber='" + key + "']");
                    row.find('.td-book-id').text(data.id);
                    var el = row.find('.delete-book');
                    row.find('.delete-book').attr('data-id', data.id);
                }
            });
        });
    }
});