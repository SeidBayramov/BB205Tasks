let deleteBtn = document.querySelectorAll(".item-delete")

deleteBtn.forEach(btn => btn.addEventListener("click", function (e) {
    e.preventDefault();

    Swal.fire({
        title: "Ar yu sur?",
        text: "luk ay em diliting haa",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "dilit it !"
    }).then((result) => {
        if (result.isConfirmed) {
            let url = btn.getAttribute("href")
            fetch(url)
                .then(response => {
                    if (response.status == 200) {
                        Swal.fire({
                            title: "ay dilitid",
                            text: "yu kent torn bek",
                            icon: "success"
                        });
                        btn.parentElement.parentElement.remove()
                    }
                    else {
                        Swal.fire({
                            title: "dilitid",
                            text: "ay sed yu",
                            icon: "error"
                        });
                    }
                })
        }
    });
}))