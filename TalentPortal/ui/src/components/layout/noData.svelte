<script>
    import nodata from "../../assets/nodata.jpg";
    import App from "../../App.svelte";
    export let message = "No data found";

    import Adddsr from "../../pages/dsr/adddsr.svelte";
</script>

<div class="noData">
    <img src={nodata} alt="No data" />
    <div class="attribution">
        <a
            href="https://www.freepik.com/free-vector/hand-drawn-no-data-concept_55024598.htm#query=no%20data%20found&position=5&from_view=search&track=ais&uuid=d6bf5295-5c4b-4fb8-89a6-377a566bab57"
            >Image by pikisuperstar</a
        >
        on Freepik
    </div>
    <p>{message}</p>
</div>

<style>
    .noData {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        height: 100%;
    }

    img {
        width: 500px; /* Adjust image width as needed */
        height: auto;
        margin-bottom: 20px; /* Add some space between image and message */
    }

    p {
        font-size: 18px; /* Adjust font size as needed */
        color: #555; /* Adjust text color as needed */
        text-align: center;
    }
    .attribution {
        font-size: 10px;
        color: #555;
        text-align: center;
    }
    img:hover + .attribution {
        display: block;
    }
    .attribution {
        display: none;
    }
</style>
