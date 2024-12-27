    // General sanitization function using DOMPurify
function sanitizeContent(content) {
    //console.log(content);
        return DOMPurify.sanitize(content);
    }

    // Modal sanitization
   /* function sanitizeModalContent(content) {
        const sanitizedContent = sanitizeContent(content);
        const modalElement = document.querySelector('#modal-body');
        if (modalElement) {
            modalElement.innerHTML = sanitizedContent;
        }
    }*/

    // Popover sanitization
    /*function sanitizePopoverContent(selector, content) {
        const sanitizedContent = sanitizeContent(content);
        $(selector).popover({
            html: true,
            content: sanitizedContent
        });
    }*/

    // Accordion sanitization
   /* function sanitizeAccordionContent(selector, content) {
        const sanitizedContent = sanitizeContent(content);
        const accordionElement = document.querySelector(selector);
        if (accordionElement) {
            accordionElement.innerHTML = sanitizedContent;
        }
    }*/

    // Global input sanitization
    /*function setSafeValue(selector, value) {
        const safeValue = sanitizeContent(value);
        const inputElement = document.querySelector(selector);
        if (inputElement) {
            inputElement.value = safeValue;
        }
    }*/

    // Dynamic sanitization for general content insertion
    /*function sanitizeDynamicContent(selector, content) {
        const sanitizedContent = sanitizeContent(content);
        const element = document.querySelector(selector);
        if (element) {
            element.innerHTML = sanitizedContent;
        }
    }*/

    // Attach global event listeners to sanitize inputs
    function attachGlobalInputSanitization() {
        document.querySelectorAll('input, select, textarea').forEach(input => {
            //console.log(input);
            input.addEventListener('input', (e) => {
                const sanitizedValue = sanitizeContent(e.target.value);
                e.target.value = sanitizedValue;
            });
        });
    }

    // Initialize sanitization for all components
    function initializeSanitization() {
        // Global input sanitization
        attachGlobalInputSanitization();
    }

    // Execute the initialization function on DOM ready
    document.addEventListener('DOMContentLoaded', () => {
        initializeSanitization();
    });
