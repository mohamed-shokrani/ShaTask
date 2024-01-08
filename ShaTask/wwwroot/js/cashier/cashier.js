function deleteCashier(id) {
    Swal.fire({
        title: 'هل أنت متأكد؟',
        text: 'سيتم حذف الكاشير نهائياً!',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'نعم، حذف',
        cancelButtonText: 'إلغاء',
    }).then((result) => {
        if (result.value) {

            $.ajax({
                url: '/Cashiers/Delete/' + id,
                type: 'POST',
                success: function (response) {
                    location.reload();
                },
                error: function (xhr, status, error) {

                    console.error(xhr.responseText);
                }
            });
        }
    });
}
function DeactivateCashier(id) {
    Swal.fire({
        title: 'هل أنت متأكد؟',
        text: 'سيتم الغاء تفعيل الكاشير نهائياً!',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'نعم، الغاء تفعيل',
        cancelButtonText: 'إلغاء',
    }).then((result) => {
        if (result.value) {

            $.ajax({
                url: '/Cashiers/DeactivateCashier/' + id,
                type: 'POST',
                success: function (response) {
                    location.reload();
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        }
    });
}