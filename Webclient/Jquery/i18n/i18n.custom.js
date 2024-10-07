// Configuration variables
const CONFIG = {
    dvLanguage: ".dvLang",
    ignoredTags: ["SCRIPT", "HTML", "IFRAME", "HEAD", "META", "STYLE", "IMG", "LINK"],
    languageStyles: {
        np: { cssFile: "Css/nepal.css", direction: "ltr" },
        fr: { cssFile: "Css/french.css", direction: "ltr" },
        ar: { cssFile: "Css/arabic.css", direction: "ltr" },
        en: { cssFile: "Css/global.css", direction: "ltr" }
    },
    inputTypes: ['text', 'password', 'submit', 'button', 'reset', 'hidden', 'search', 'url', 'tel', 'file', 'range', 'time', 'date', 'number', 'email'],
    dataAttributes: {
        value: "data-i18n-value",
        placeholder: "data-i18n-placeholder",
        text: "data-i18n"
    },
    inputAttribtues: {
        input: 'INPUT',
        textarea: 'TEXTAREA'
    },
    maxKeyLength: 25,
    defaultLanguage: "en",
    triggerElementTypes: ['button', 'input[type="submit"]'],
    prefix: "ABC-",
    vcDataParentClass: '.dvVcData',
    errorsParentClass: '.dvErrors',
    jqueryUiSelectMenuParentClass: '.dvJqueryUiSelectMenu',
    jqueryUiSelectMenuUiSelectMenuText: '.dvJqueryUiSelectMenu .ui-selectmenu-text',
    jqueryUiSelectMenuAllSelect: '.dvJqueryUiSelectMenu select'
};

// Function to set data-i18n attributes for text content elements
function setDataI18nAttributes(elements) {
    elements.forEach((element) => {
        if (
            element.children.length === 0 &&
            !element.hasAttribute(CONFIG.dataAttributes.text) &&
            element.textContent.trim() !== "" &&
            !CONFIG.ignoredTags.includes(element.tagName)
        ) {
            let textContent = element.textContent.trim().toLowerCase().replace(/\s+/g, "-");
            textContent = textContent.substring(0, CONFIG.maxKeyLength);
            element.setAttribute(CONFIG.dataAttributes.text, `${CONFIG.prefix}${textContent}`);
        }
    });
}

// Function to set data-i18n attributes for input elements' value, placeholder, onfocus, and onblur
function setInputValueDataI18nAttributes(inputs) {
    inputs.forEach((input) => {
        // Handle value attribute
        if (input.hasAttribute("value") && !input.hasAttribute(CONFIG.dataAttributes.value)) {
            let valueContent = input.getAttribute("value").trim().toLowerCase().replace(/\s+/g, "-");
            valueContent = valueContent.substring(0, CONFIG.maxKeyLength);
            input.setAttribute(CONFIG.dataAttributes.value, `${CONFIG.prefix}${valueContent}`);
        }

        // Handle placeholder attribute
        if (input.hasAttribute("placeholder") && !input.hasAttribute(CONFIG.dataAttributes.placeholder)) {
            let placeholderContent = input.getAttribute("placeholder").trim().toLowerCase().replace(/\s+/g, "-");
            placeholderContent = placeholderContent.substring(0, CONFIG.maxKeyLength);
            input.setAttribute(CONFIG.dataAttributes.placeholder, `${CONFIG.prefix}${placeholderContent}`);
        }

        // Handle onfocus attribute
        if (input.hasAttribute("onfocus") && !input.hasAttribute("data-i18n-onfocus")) {
            let onfocusContent = input.getAttribute("onfocus").trim().toLowerCase().replace(/\s+/g, "-");
            onfocusContent = onfocusContent.substring(0, CONFIG.maxKeyLength);
            input.setAttribute("data-i18n-onfocus", `${CONFIG.prefix}${onfocusContent}`);
        }

        // Handle onblur attribute
        if (input.hasAttribute("onblur") && !input.hasAttribute("data-i18n-onblur")) {
            let onblurContent = input.getAttribute("onblur").trim().toLowerCase().replace(/\s+/g, "-");
            onblurContent = onblurContent.substring(0, CONFIG.maxKeyLength);
            input.setAttribute("data-i18n-onblur", `${CONFIG.prefix}${onblurContent}`);
        }
    });
}


// Function to load translations from a JSON file
function loadTranslations(lang, callback) {
    const filePath = `../i18n/${lang}/${lang}.json`;
    fetch(filePath)
        .then((response) => response.json())
        .then((translations) => {
            callback(translations);
        })
        .catch((error) => {
            console.error(`Error loading ${filePath}:`, error);
        });
}

// Function to apply translations to elements
function applyTranslations(elements, translations) {
    elements.forEach((element) => {
        const key = element.getAttribute(CONFIG.dataAttributes.text);
        if (key && translations[key]) {
            element.textContent = translations[key];
        }
    });
}

// Function to apply translations to input elements' value, placeholder, onfocus, and onblur
function applyInputTranslations(inputs, translations) {
    inputs.forEach((input) => {
        const valueKey = input.getAttribute(CONFIG.dataAttributes.value);
        const placeholderKey = input.getAttribute(CONFIG.dataAttributes.placeholder);

        let originalValue = input.value; // Store the original value
        let userModified = false; // Track if the user has modified the value

        if (valueKey && translations[valueKey]) {
            input.value = translations[valueKey];
            originalValue = translations[valueKey]; // Set the original to the translated value
        }

        if (placeholderKey && translations[placeholderKey]) {
            input.placeholder = translations[placeholderKey];
        }

        input.addEventListener("focus", function () {
            originalValue = input.value; // Update the original value on focus
            userModified = false; // Reset the flag when the input gains focus
        });

        input.addEventListener("input", function () {
            userModified = true; // Mark as modified when the user types
        });

        input.addEventListener("blur", function () {
            if (!userModified && valueKey && translations[valueKey]) {
                input.value = translations[valueKey]; // Only set back to translated value if not modified
            }
        });
    });
}




// Function to handle language-specific styles
function handleLanguageSpecificStyles(lang) {
    const head = document.querySelector("head");
    let linkTag = document.getElementById("languageStyles");

    if (CONFIG.languageStyles[lang]) {
        if (!linkTag) {
            linkTag = document.createElement("link");
            linkTag.id = "languageStyles";
            linkTag.rel = "stylesheet";
            head.appendChild(linkTag);
        }
        linkTag.href = CONFIG.languageStyles[lang].cssFile;
        document.body.style.direction = CONFIG.languageStyles[lang].direction;
    } else {
        if (linkTag) {
            linkTag.remove();
        }
        document.body.style.direction = "ltr";
    }
}

// Function to set data-i18n attributes for jQuery UI selectmenu items
function setJqueryUiSelectMenuDataI18n() {
    const selectMenus = document.querySelectorAll(CONFIG.jqueryUiSelectMenuUiSelectMenuText);

    selectMenus.forEach(span => {
        if (!span.hasAttribute(CONFIG.dataAttributes.text)) {
            let textContent = span.textContent.trim().toLowerCase().replace(/\s+/g, "-");
            textContent = textContent.substring(0, CONFIG.maxKeyLength);
            if (textContent) {
                span.setAttribute(CONFIG.dataAttributes.text, `${CONFIG.prefix}${textContent}`);
            }
        }
    });
}

// Function to apply translations to jQuery UI selectmenu items
function applySelectMenuTranslations(translations) {
    const selectMenus = document.querySelectorAll(CONFIG.jqueryUiSelectMenuUiSelectMenuText);

    selectMenus.forEach(span => {
        const key = span.getAttribute(CONFIG.dataAttributes.text);
        if (key && translations[key]) {
            span.textContent = translations[key];
        }
    });
}

// Function to set data-i18n attributes for span elements inside .dvErrors
function setErrorMessagesDataI18n() {
    const errorDivs = document.querySelectorAll(CONFIG.errorsParentClass);

    errorDivs.forEach(div => {
        const spans = div.querySelectorAll('span');
        spans.forEach(span => {
            // Check if the span already has a data-i18n attribute
            if (!span.hasAttribute(CONFIG.dataAttributes.text)) {
                // Set the data-i18n attribute based on the text content
                let textContent = span.textContent.trim().toLowerCase().replace(/\s+/g, "-");
                textContent = textContent.substring(0, CONFIG.maxKeyLength);
                if (textContent) {
                    span.setAttribute(CONFIG.dataAttributes.text, `${CONFIG.prefix}${textContent}`);
                }
            }
        });
    });
}

// Function to apply translations to error messages
function applyErrorTranslations() {
    const errorDivs = document.querySelectorAll(CONFIG.errorsParentClass);
    const lang = localStorage.getItem("selectedLanguage") || CONFIG.defaultLanguage;

    loadTranslations(lang, (translations) => {
        errorDivs.forEach(div => {
            const spans = div.querySelectorAll('span[data-i18n]');
            spans.forEach(span => {
                const key = span.getAttribute(CONFIG.dataAttributes.text);
                if (key && translations[key]) {
                    span.textContent = translations[key];
                }
            });
        });
    });
}

// Function to set data-i18n attributes and apply translations for elements within .dvVcData
function updateVcDataSections() {
    const vcDataSections = document.querySelectorAll(CONFIG.vcDataParentClass);
    vcDataSections.forEach(section => {
        const elements = section.querySelectorAll('*');
        const inputs = section.querySelectorAll(`input[type='${CONFIG.inputTypes.join("'], input[type='")}'], textarea`);
        setDataI18nAttributes(elements);
        setInputValueDataI18nAttributes(inputs);
        loadTranslations(localStorage.getItem("selectedLanguage") || CONFIG.defaultLanguage, (translations) => {
            applyTranslations(elements, translations);
            applyInputTranslations(inputs, translations);
        });
    });
}


// Function to initialize event listeners for trigger elements
function initializeTriggerElements() {
    CONFIG.triggerElementTypes.forEach(selector => {
        document.querySelectorAll(selector).forEach(element => {
            element.addEventListener('click', () => {
                // Apply data-i18n attributes to error messages
                setErrorMessagesDataI18n();

                // Apply translations to error messages
                applyErrorTranslations();
            });
        });
    });
}

// Function to initialize language switching
function initializeLanguageSwitching(select, elements, inputElements) {
    const storedLang = localStorage.getItem("selectedLanguage") || CONFIG.defaultLanguage;
    select.value = storedLang;
    loadTranslations(storedLang, (translations) => {
        applyTranslations(elements, translations);
        applyInputTranslations(inputElements, translations);
        setJqueryUiSelectMenuDataI18n(); // Ensure select menu items have data-i18n attributes
        applySelectMenuTranslations(translations); // Apply translations to select menus
        setErrorMessagesDataI18n(); // Ensure error messages have data-i18n attributes
        applyErrorTranslations(); // Apply translations to error messages
        updateVcDataSections(); // Update dynamic sections

        // Refresh jQuery UI selectmenu to reflect the new translations
        $(CONFIG.jqueryUiSelectMenuAllSelect).each(function () {
            const $menu = $(this).selectmenu();
            $menu.selectmenu("refresh"); // Ensure the selectmenu is refreshed
        });
    });
    handleLanguageSpecificStyles(storedLang);

    select.addEventListener("change", function () {
        const lang = this.value;
        localStorage.setItem("selectedLanguage", lang);
        loadTranslations(lang, (translations) => {
            applyTranslations(elements, translations);
            applyInputTranslations(inputElements, translations);
            setJqueryUiSelectMenuDataI18n(); // Ensure select menu items have data-i18n attributes
            applySelectMenuTranslations(translations); // Apply translations to select menus
            setErrorMessagesDataI18n(); // Ensure error messages have data-i18n attributes
            applyErrorTranslations(); // Apply translations to error messages
            updateVcDataSections(); // Update dynamic sections

            // Refresh jQuery UI selectmenu to reflect the new translations
            $(CONFIG.jqueryUiSelectMenuAllSelect).each(function () {
                const $menu = $(this).selectmenu();
                $menu.selectmenu("refresh"); // Ensure the selectmenu is refreshed
            });
        });
        handleLanguageSpecificStyles(lang);
    });
}


// Function to handle changes in selectmenu
function initializeSelectMenuEventHandlers() {
    $(CONFIG.jqueryUiSelectMenuParentClass).each(function () {
        const $menu = $(this).find("select").selectmenu();

        $menu.on("selectmenuchange", function () {
            // Call the function to update data-i18n attributes
            setJqueryUiSelectMenuDataI18n();
        });
    });
}

/*-X-X-X-X-X-X-X-X-X-X-X-X THIS CODE IS ONLY FOR MANAGING LOCALE URL -X-X-X-X-X-X-X-X-X-X-X-X*/
// Function to get the locale from the URL
function getLocaleFromUrl() {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get('Locale') || null;
}

// Function to set the locale in the URL without reloading the page
function setLocaleInUrl(locale) {
    const urlParams = new URLSearchParams(window.location.search);
    urlParams.set('Locale', locale);
    const newUrl = window.location.pathname + '?' + urlParams.toString();
    window.history.replaceState({}, '', newUrl);
}

// Function to update the page language and URL based on the locale
function updateLanguage(locale) {
    // Store the selected language in localStorage
    localStorage.setItem("selectedLanguage", locale);

    // Update the URL to reflect the selected language
    setLocaleInUrl(locale);

    // Update the language in the select menu
    const select = document.querySelector(CONFIG.dvLanguage);
    if (select) {
        select.value = locale;
    }

    // Load the corresponding translations and apply them
    loadTranslations(locale, (translations) => {
        const bodyElements = document.querySelectorAll("body *");
        const inputElements = document.querySelectorAll(`input[type='${CONFIG.inputTypes.join("'], input[type='")}']`);

        applyTranslations(bodyElements, translations);
        applyInputTranslations(inputElements, translations);
        setJqueryUiSelectMenuDataI18n(); // Ensure select menu items have data-i18n attributes
        applySelectMenuTranslations(translations); // Apply translations to select menus
        setErrorMessagesDataI18n(); // Ensure error messages have data-i18n attributes
        applyErrorTranslations(); // Apply translations to error messages
        updateVcDataSections(); // Update dynamic sections

        // Refresh jQuery UI selectmenu to reflect the new translations
        $(CONFIG.jqueryUiSelectMenuAllSelect).each(function () {
            const $menu = $(this).selectmenu();
            $menu.selectmenu("refresh"); // Ensure the selectmenu is refreshed
        });
    });

    handleLanguageSpecificStyles(locale);
    BindBanner();
}

// Function to handle the initial page load and setup
function manageLocaleOnLoad() {
    const currentLocale = getLocaleFromUrl();
    const storedLocale = localStorage.getItem("selectedLanguage") || CONFIG.defaultLanguage;

    if (currentLocale) {
        updateLanguage(currentLocale);
    } else {
        updateLanguage(storedLocale);
    }

    // Set up the language change handler
    document.querySelector(CONFIG.dvLanguage).addEventListener('change', function () {
        const selectedLocale = this.value;
        updateLanguage(selectedLocale);
    });
}
/*-X-X-X-X-X-X-X-X-X-X-X-X THIS CODE IS ONLY FOR MANAGING LOCALE URL -X-X-X-X-X-X-X-X-X-X-X-X*/

// Function to generate and download the JSON file with data-i18n attributes
function downloadTranslations() {
    const elements = document.querySelectorAll("body *");
    const translations = {};

    elements.forEach((element) => {
        const key = element.getAttribute(CONFIG.dataAttributes.text);
        if (key) {
            translations[key] = element.textContent.trim();
        }
        // Get translation for input value and placeholder
        if (element.tagName === CONFIG.inputAttribtues.input || element.tagName === CONFIG.inputAttribtues.textarea) {
            const valueKey = element.getAttribute(CONFIG.dataAttributes.value);
            if (valueKey) {
                translations[valueKey] = element.value.trim();
            }

            const placeholderKey = element.getAttribute(CONFIG.dataAttributes.placeholder);
            if (placeholderKey) {
                translations[placeholderKey] = element.placeholder.trim();
            }
        }
    });

    const dataStr = JSON.stringify(translations, null, 2);
    const blob = new Blob([dataStr], { type: "application/json" });
    const url = URL.createObjectURL(blob);
    const a = document.createElement("a");
    a.href = url;
    a.download = "translations.json";
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
}


// Initialize the script on window load
window.onload = function () {
    manageLocaleOnLoad();

    const select = document.querySelector(CONFIG.dvLanguage);
    const bodyElements = document.querySelectorAll("body *");
    const inputElements = document.querySelectorAll(`input[type='${CONFIG.inputTypes.join("'], input[type='")}']`);

    loadTranslations(localStorage.getItem("selectedLanguage") || CONFIG.defaultLanguage, (translations) => {
        setDataI18nAttributes(bodyElements);
        setInputValueDataI18nAttributes(inputElements);
        setJqueryUiSelectMenuDataI18n(); // Add this line to set data-i18n for select menus
        initializeLanguageSwitching(select, bodyElements, inputElements);
        initializeTriggerElements(); // Initialize trigger elements for error messages
        //updateVcDataSections(); // load this function only after data is fetched in there respective ajax call
    });

    const downloadButton = document.getElementById("downloadTranslations");
    if (downloadButton) {
        downloadButton.addEventListener("click", downloadTranslations);
    }

    // Initialize selectmenu event handlers
    initializeSelectMenuEventHandlers();
};
