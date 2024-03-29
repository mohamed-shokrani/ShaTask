﻿const invoiceDetails = [];
$(document).ready(function () {
   
        $("#add-new").click(function () {
            let newRow = $(".show-items").first().clone();
            console.log(newRow.children());
            newRow.find("label").remove();
            newRow.find("input").val("");
            $("#item-continer").append(newRow);
        });
       
        $(document).on("click", ".delete-btn", function (e) {
            e.preventDefault(); 
            if ($("#item-continer").children().length >1) {
                $(this).closest(".show-items").remove();
              
            }
            updateTotal();
        });

        $(document).on('change', '.itemCount, .itemPrice', function () {
           
        const quantity = parseInt($(this).closest(".row").find(".itemCount").val());
    const price = parseFloat($(this).closest(".row").find(".itemPrice").val());
            let total = 0;
            if (!Number.isNaN(quantity) && !Number.isNaN(price)) {
                 total = quantity * price;
            }
            $(this).closest(".row").find("#totalForEachItem").val(total.toFixed(2));
            updateTotal();
        });

        function updateTotal() {
            let totalcost = 0;
            $('.total').each(function (index, ele) {
                let num = parseFloat($(ele).val());
                totalcost += num;
                

                if (totalcost > 0)
                    $("#total-cost").text(totalcost );
                
            });
        }
         $("#generate-inv").on('click' ,function (e) {
      
             e.preventDefault();
             if ($('form')[0].checkValidity()) {
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
        let now = new Date();

        let formattedDate = now.toISOString().split('T')[0];

        document.getElementById('dateInput').value = formattedDate;
       
    });
