<script>
// @ts-nocheck

    export let size = "medium"; // Options: "small", "medium", "large"
    export let color = "#007bff"; // Default color is blue

    const spinnerSizes = {
        small: "16px",
        medium: "32px",
        large: "64px"
    };

    const spinnerColor = {
        border: `2px solid ${color}`,
        borderTop: `2px solid transparent`
    };
</script>

<div class="loader" style="width: {spinnerSizes[size]}; height: {spinnerSizes[size]};">
    <div class="spinner" style={spinnerColor}></div>
</div>

<style>
    .loader {
        display: flex;
        align-items: center;
        justify-content: center;
    }

    .spinner {
        width: 100%;
        height: 100%;
        border-radius: 50%;
        animation: spin 1s linear infinite;
    }

    @keyframes spin {
        0% {
            transform: rotate(0deg);
        }
        100% {
            transform: rotate(360deg);
        }
    }
</style>
