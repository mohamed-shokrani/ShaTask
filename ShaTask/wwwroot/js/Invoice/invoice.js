$(document).ready(function () {


    var invoiceDetails = [];
    let CustomerName = $('#CustomerName').val();
    let BranchId = $('#BranchId').val();

    $('#item-form').on('click', function (event) {
        event.preventDefault();

        let itemName = $('#itemName').val();
        let itemCount = $('#itemCount').val();
        let itemPrice = $('#itemPrice').val();

        if (itemName !== "" && itemCount !== "" && itemPrice !== "") {
            var newItem = {
                ItemName: itemName,
                ItemCount: parseInt(itemCount),
                ItemPrice: parseFloat(itemPrice)
            };

            invoiceDetails.push(newItem);

            console.log(invoiceDetails)
            var newIndex = invoiceDetails.length - 1;
            updateCartTable(newItem, newIndex);
            updateTotalCost();
        }
    });

    function updateCartTable(newItem, index) {
        $('#cart-table tbody').append(
            "<tr data-index='" + index + "'><td>" + newItem.ItemName + "</td><td>" + newItem.ItemCount +
            "</td><td>" + newItem.ItemPrice.toFixed(2) +
            "</td><td><button class='btn btn-danger'><i class='fa fa-trash'></i></button></td></tr>"
        );
    }

    function updateTotalCost() {
        var total = invoiceDetails.reduce(function (acc, item) {
            return acc + item.ItemCount * item.ItemPrice;
        }, 0);
        $("#totalCost").text(total.toFixed(2));
    }


    $('#cart-table').on('click', '.btn-danger', function () {
        var row = $(this).closest('tr');
        var index = row.index();
        invoiceDetails.splice(index, 1);
        row.remove();
        updateTotalCost();
    });


    $('#generateInvoice').on('click', function () {

        var serializedData = JSON.stringify(invoiceDetails);

        $('#invoiceDetailsJson').val(serializedData);

        // Submit the form
        $('form').submit();
    });
})