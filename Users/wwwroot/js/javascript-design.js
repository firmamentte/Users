
window.onresize = (e) => {

    e.preventDefault()

    reCenterPopupFormSmall()
}

const isVisible = (element) => {

    if (!!element) {

        const _style = window.getComputedStyle(element)

        return (
            _style.opacity !== "0" &&
            _style.display !== "none" &&
            _style.visibility !== "hidden")
    }
    else {
        return false
    }
}

const removeLineBreaks = (string) => {
    return string.replace(/\s{2,}/g, "")
}

const handleError = (response) => {

    if (!response.ok) {
        return response.text().
            then((error) => {
                throw Error(error)
            })
    }
    return response
}

const htmlDataType = (response) => {
    return response.text()
}

const postOptions = (body) => {
    return {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8"
        },
        body: body
    }
}

const isErrorMessageDivEmpty = (errorMessageDiv) => {

    if (!!errorMessageDiv.innerHTML) {
        return false
    }
    else {
        return true
    }
}

const serialize = (form) => {

    const _serialized = []

    for (let i = 0; i < form.elements.length; i++) {

        const _field = form.elements[i]

        if (!_field.name ||
            ////_field.disabled ||
            _field.type === "file" ||
            _field.type === "reset" ||
            _field.type === "submit" ||
            _field.type === "button")
            continue

        if (_field.type === "select-multiple") {

            for (let n = 0; n < _field.options.length; n++) {

                if (!_field.options[n].selected)
                    continue

                _serialized.push(`${encodeURIComponent(_field.name)}=${encodeURIComponent(_field.options[n].value.trim())}`)
            }
        }
        else if (_field.type !== "checkbox" && _field.type !== "radio") {
            _serialized.push(`${encodeURIComponent(_field.name)}=${encodeURIComponent(_field.value.trim())}`)
        } else if (_field.checked) {
            _serialized.push(`${encodeURIComponent(_field.name)}=${encodeURIComponent(_field.checked)}`)
        }
    }

    return _serialized.join("&")
}

const clearErrorMessageDiv = (errorMessageDiv) => {
    errorMessageDiv.innerHTML = ""
    return errorMessageDiv
}

const appendErrorMessage = (errorMessageDiv, errorMessageText) => {
    errorMessageDiv.insertAdjacentHTML("beforeend", `&#8594; ${errorMessageText}<br />`)
}

const showPopupFormHtml = (data) => {

    document.querySelector("#popupFormToShow").innerHTML = data

    let _popupForm

    if (!!document.querySelector(".popup-form-small")) {
        _popupForm = document.querySelector(".popup-form-small")

        centerPopupFormSmall(_popupForm)
    }
    else {
        if (!!document.querySelector(".popup-form-medium")) {
            _popupForm = document.querySelector(".popup-form-medium")

            _popupForm.style.cssText = `top:${window.pageYOffset}px`
        }
    }

    if (!!_popupForm) {

        showPopupBackground()

        _popupForm.classList.remove("hide-popup-form")
        _popupForm.classList.add("show-popup-form")
    }
}

const hidePopupForm = () => {

    let _popupForm

    if (!!document.querySelector(".popup-form-small")) {
        _popupForm = document.querySelector(".popup-form-small")
    }

    if (!!document.querySelector(".popup-form-medium")) {
        _popupForm = document.querySelector(".popup-form-medium")
    }

    if (!!_popupForm) {

        _popupForm.classList.remove("show-popup-form")
        _popupForm.classList.add("hide-popup-form")

        hidePopupBackground()
    }
}

const toggleButtonProgressBar = (buttonDiv, progressBar) => {

    buttonDiv.classList.toggle("hide-button-progress-bar")
    progressBar.classList.toggle("show-button-progress-bar")
}

const showErrorPopupForm = (error) => {

    fetch(`/Shared/Ok?okMessage=${error}&messageSymbol=x`).
        then(handleError).
        then(htmlDataType).
        then((data) => {
            showPopupFormHtml(data)
        }).
        catch((error) => {
            console.log(error)
        })
}

const yesNoConfirmation = (actionController, actionMethod, actionValue, yesNoMessage) => {

    const _parameters =
        `?actionController=${actionController}
         &actionMethod=${actionMethod}
         &actionValue=${actionValue}
         &yesNoMessage=${yesNoMessage}`

    fetch(`/Shared/YesNo${removeLineBreaks(_parameters)}`).
        then(handleError).
        then(htmlDataType).
        then((data) => {
            showPopupFormHtml(data)
        }).
        catch((error) => {
            showErrorPopupForm(error)
        })
}

const onBeginYesNo = () => {
    toggleButtonProgressBar(document.querySelector("#navYesNo"), document.querySelector("#progressBarYesNo"))
}

function hidePopupBackground() {

    const _backgroundPopup = document.querySelector(".background-popup")

    _backgroundPopup.classList.remove("show-popup-background")
    _backgroundPopup.classList.add("hide-popup-background")
}

function showPopupBackground() {

    const _backgroundPopup = document.querySelector(".background-popup")

    _backgroundPopup.classList.remove("hide-popup-background")
    _backgroundPopup.classList.add("show-popup-background")
}

function centerPopupFormSmall(popupForm) {

    const _windowWidth = document.documentElement.clientWidth,
        _popupWidth = popupForm.clientWidth

    popupForm.style.cssText =
        `top:${window.pageYOffset + 10}px;left:${_windowWidth / 2 - _popupWidth / 2}px`
}

const reCenterPopupFormSmall = () => {

    const _popupForm = document.querySelector(".popup-form-small")

    if (isVisible(_popupForm)) {
        centerPopupFormSmall(_popupForm)
    }
}

const gridViewMessage = (message) => {
    return `<div class="gv-row-message">
            <span class="gv-message-value">${message}</span>
            </div>`
}