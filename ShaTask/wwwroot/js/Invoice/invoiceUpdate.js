
// Wait for the document to be ready
const invoiceDetails = [];
  
    $(document).ready(function () {
        // Add event listener for the "Add New" button
        $("#add-new").click(function () {
            // Clone the first row with all its children
            var newRow = $(".show-items").first().clone();

            // Clear input values in the cloned row
            newRow.find("input").val("");
            newRow.find("label").remove("");

            // Append the cloned row to the end of the container
            $("#item-container").append(newRow);
        });
       
       

    // Add event listener for changes in item count or price
        $(document).on('change', '.itemCount, .itemPrice', function () {
            var totalcost = 0;
            // Get the quantity and price of the current item
        const quantity = parseInt($(this).closest(".row").find(".itemCount").val());
    const price = parseFloat($(this).closest(".row").find(".itemPrice").val());
            // Calculate the total for this item
            let total = 0;
            if (!Number.isNaN(quantity) && !Number.isNaN(price)) {
                 total = quantity * price;
            }
    // Update the total input field for this item
            $(this).closest(".row").find("#totalForEachItem").val(total.toFixed(2));
            $('.total').each(function (index, ele) {
                let num = parseFloat($(ele).val());
                totalcost += num;
               // console.log(totalcost);
                console.log(total);

                if (totalcost >0)
                    $("#total-cost").val("totalPrice:"+" " + totalcost);
                else{
                    $("#total-cost").val("totalPrice");

                }
            });
        });

        // Add event listener for form submission

         $("#generate-inv").on('click' ,function (e) {
        //$("#generate-inv").on('click', function () {
            // Calculate the total price of all items

             // Push details of each item into invoiceDetails array
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
                 // If the form is not valid, focus on the first invalid input field
                 $('form')[0].reportValidity();
             }
        });
       
    });
