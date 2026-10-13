export function registerEscapeHandler(dotNetReference) {
    const handler = (event) => {
        if (event.key === "Escape") {
            dotNetReference.invokeMethodAsync("HandleEscape");
        }
    };

    document.addEventListener("keydown", handler);

    return {
        dispose: () => {
            document.removeEventListener("keydown", handler);
        }
    };
}