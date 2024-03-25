function deleteInvoice(id) {
    Swal.fire({
        title: 'هل أنت متأكد؟',
        text: 'سيتم حذف الفاتورة !',
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: 'نعم، حذف',
        cancelButtonText: 'إلغاء',
    }).then((result) => {
        if (result.value) {

            $.ajax({
                url: '/invoices/delete/' + id,
                type: 'POST',
                success: function (message) {
                    Swal.fire({
                        title: message,
                        text: message,
                        icon: "success"
                    })
                    setTimeout(function () {
                        location.reload();
                    }, 1000);
                },
                error: function (xhr, status, error) {

                    console.error(xhr.responseText);
                }
            });
        }
    });
}