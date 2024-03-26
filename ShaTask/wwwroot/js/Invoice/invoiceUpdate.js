$(document).ready(function () {
    
        $("#add-new").click(function () {
            let newRow = $(".show-items").first().clone();
            newRow.find("label").remove();
            newRow.find("input").val("");
            $("#item-container").append(newRow);
        });

    $("#item-container").children().slice(1).find("label").remove();
        $(document).on('change', '.itemCount, .itemPrice', function () {
            var totalcost = 0;
            // Get the quantity and price of the current item
        const quantity = parseInt($(this).closest(".row").find(".itemCount").val());
        const price = parseFloat($(this).closest(".row").find(".itemPrice").val());
            let total = 0;
            if (!Number.isNaN(quantity) && !Number.isNaN(price)) {
                 total = quantity * price;
            }
            $(this).closest(".row").find("#totalForEachItem").val(total.toFixed(2));
            $('.total').each(function (index, ele) {
                let num = parseFloat($(ele).val());
                totalcost += num;
                console.log(total);

                if (totalcost >0)
                    $("#total-cost").val("totalPrice:"+" " + totalcost);
                else{
                    $("#total-cost").val("totalPrice");

                }
            });
            updateTotal();
        });
        $(document).on("click", ".delete-btn", function (e) {
            e.preventDefault();
            console.log($("#item-container").children().length)
            if ($("#item-container").children().length > 1) {
                $(this).closest(".show-items").remove();
                console.log("true")
            }
            updateTotal();
        });

      
    $("#generate-inv").on('click', function (e) {

        e.preventDefault();
        if ($('form')[0].checkValidity()) {
            let invoiceDetails = [];
            if (invoiceDetails.length >0) {
                invoiceDetails = []
            }
            $(".show-items").each(function () {
                const itemName = $(this).closest(".row").find(".itemName").val();
                const itemCount = parseInt($(this).closest(".row").find(".itemCount").val());
                const itemPrice = parseFloat($(this).closest(".row").find(".itemPrice").val());
                invoiceDetails.push({ itemName: itemName, itemCount: itemCount, itemPrice: itemPrice });
            });
            let serializeDate = JSON.stringify(invoiceDetails);
            $("#invoiceDetailsJson").val(serializeDate);
            console.log(invoiceDetails);
            $('form').submit();
        }
        else {
            $('form')[0].reportValidity();
        }
    });
        function updateTotal() {
            let totalcost = 0;
            $('.total').each(function (index, ele) {
                let num = parseFloat($(ele).val());
                totalcost += num;


                if (totalcost > 0)
                    $("#total-cost").text(totalcost);
             
            });
        }
       
    });
