export function initConsole(selector) {
    const el = document.querySelector(selector);
    if (el) {
        el.innerHTML = `
            <div style="font-family: monospace; background: #111; color: #0f0; padding: 10px;">
                Claude Console Initialized!
            </div>
        `;
    }
}
