const genericModal = "genericModal";
const modalFormElementId = "modalForm";
const modalElement = document.getElementById(genericModal);
const modalContentElement = modalElement.querySelector("#modalContent");

function formatCurrency(amountString) {
    const currency = Intl.NumberFormat("en-us", { style: "currency", currency: "USD" });

    let amount = parseFloat(amountString);
    if (amount === NaN) {
        amount = 0;
    }

    currency.format(amount);
}

function addModalListeners() {
    const links = document.getElementsByClassName(genericModal);
    Array.from(links).forEach(link => {
        link.addEventListener('click', function (event) {
            event.preventDefault();
            showGenericModal(this);
        });
    });
}

async function showGenericModal(element) {
    if (!modalElement) {
        return;
    }

    const modal = new bootstrap.Modal(modalElement, { backdrop: "static" });
    modalContentElement.innerHTML = "<div>Loading...</div>";
    const url = element.getAttribute("data-url");
    fetch(url)
        .then((response) => {
            if (!response.ok) {
                throw new Error();
            };
            return response.text()
        })
        .then(data => {
            modalContentElement.innerHTML = data;
            modal.show();
            formElement = document.getElementById(modalFormElementId);

            if (formElement != null) {
                formElement.addEventListener('submit', function (event) { event.preventDefault(); });
            }
        })
        .catch(error => {
            console.error(error);
            alert('Failed to load modal.');
        });
}

async function submitGenericModalAsync(submitButtonElement) {
    submitButtonElement.disabled = true;
    let formElement = document.getElementById(modalFormElementId);
    const url = formElement.getAttribute("action");
    const formData = new FormData(formElement);
    const data = new URLSearchParams(formData);

    const response = await fetch(url, { method: "post", body: data });
    if (response.ok) {
        const html = await response.text();
        if (html.length === 0) {
            location.reload(true); // no content is returned when the submission was successful; refresh the page
            return;
        }
        modalContentElement.innerHTML = html;
        formElement = document.getElementById(modalFormElementId);
        formElement.addEventListener('submit', function (event) { event.preventDefault(); });
    }
    else {
        console.error(response);
        alert("An unexpected error occurred. If this message persists, please contact help support.");
    }

    submitButtonElement.disabled = false;
}

addModalListeners();