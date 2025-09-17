/**
 * This module is used to initialize the Blazor WebAssembly app.
 * Use a link in your index.html file to import this module:
 * <script type="module" src="indexinit.js"></script>
 */

window.isLoaded = false; // Used by program.cs to determine if the app is ready to start.
window.checkIfLoaded = function () {
    return window.isLoaded;
};


if (window.location.origin.includes("localhost")) {
    /*
    * For localhost development, the app code is served by the localhost server. However, 
    * the app calls the cloud for static assets. The app may make service calls against either
    *  the cloud application or the localhost application api.
    */

    try {
        /*** APP LOADED FROM THE LOCALHOST ***/
        console.debug("Running from local development host");
        const { appConfig } = await import('./_content/BlazorUI/appConfig.js');
        window.appConfig = {
            appPath: "/", // When running from localhost, the WASM app is at the root.
            appUrl: window.location.origin,
            androidAppUrl: "",
            remoteApiUrl: appConfig.remoteApiUrl,
            localApiUrl: appConfig.localApiUrl,
            assetsUrl: appConfig.assetsUrl,
            authConfigName: appConfig.authConfigName,
        };

    } catch (error) {
        console.error("Error loading appConfig.js:", error);
    }
} else {
    /**** APP LOADED FROM NON-DEV HOST (cloud, remote host etc.) ****/
    // When runing from the cloud, the baseHref is set to the base URL of the app.
    console.log("=== INDEXINIT.JS PATH DETECTION ===");
    console.log("Current URL:", window.location.href);
    console.log("Current pathname:", window.location.pathname);
    
    // Open the appConfig.js file to get the configured app path
    const { appConfig } = await import('./_content/BlazorUI/appConfig.js');
    
    // Use the app path from configuration rather than trying to detect it from the URL
    // This prevents issues during authentication callbacks where the URL context might be wrong
    let appPath = appConfig.appPath || '/baseapp/';
    console.log("AppPath from appConfig:", appPath);
    
    // Log current URL context for debugging
    const currentPathSegments = window.location.pathname.split('/').filter(segment => segment !== '');
    console.log("Current path segments:", currentPathSegments);
    
    const baseHrefElement = document.querySelector('base');
    if (baseHrefElement) {
        console.log("Base element href:", baseHrefElement.href);
    }
    
    console.log("Final AppPath: " + appPath);
    console.log("=== END PATH DETECTION ===");
    window.appConfig = {
        appPath: appPath,
        appUrl: window.location.origin + "/",
        androidAppUrl: "",
        remoteApiUrl: window.location.origin + "/",
        localhostApiUrl: "", // We do not set localApiUrl because the app has no access to localhost.
        assetsUrl: window.location.origin + "/",
        wsUrl: window.location.origin.replace(/^http/, 'ws') + "/",
        authConfigName: appConfig.authConfigName,
    };

    if (navigator.serviceWorker) {
        console.log("Registering service worker");
        navigator.serviceWorker.addEventListener('message', event => {
            console.log("message:" + event.data.action + "," + event.data.info);
            switch (event.data.action) {
                case 'CacheMiss':
                    const logTextElement = document.getElementById('logText');
                    const logText = logTextElement.textContent + `\n${event.data.action}, ${event.data.info}`;

                    if (logTextElement) {
                        logTextElement.textContent = logText;
                    }
                    break;
            }
        });
        // Note that the service worker activate event kicks off the asset caching process.
        // During publish, service-worker.published.js is copied to service-worker.js
        // Ensure the scope ends with a trailing slash and doesn't include sub-paths
        navigator.serviceWorker.register('service-worker.js', { type: 'module', scope: appPath });
    }
}

window.isLoaded = true; // Let program.cs know that the app is ready to start.
