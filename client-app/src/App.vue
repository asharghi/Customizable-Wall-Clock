<template>
  <div class="fullsize">
    <div v-if="showClockOnly">
      <Clock
        :dotMinutesSettings="dotMinutesSettings"
        :dotFifthSettings="dotMinutesSettings"
        :dotQuarterSettings="dotMinutesSettings"
        backgroundImage="yellow"
        :size="800"
      />
    </div>
    <div v-else class="settings">
      <DotSettingsBuilder 
      :settings="dotMinutesSettings"
      @change="dotMinutesSettingsChanged" />
    </div>
  </div>
</template>

<script>
import Clock from "./components/Clock.vue";
import DotSettingsBuilder from "./components/DotSettingsBuilder.vue";
const signalR = require("@aspnet/signalr");

export default {
  components: {
    Clock,
    DotSettingsBuilder
  },
  data() {
    return {
      showClockOnly: false,
      connection: "",
      messages: [],
      dotMinutesSettings: {
        space: 20,
        width: 5,
        height: 5,
        radius: 5,
        hue: 50,
        luminosity: 50,
        active: false
      }
    };
  },
  methods: {
    dotMinutesSettingsChanged(newSettings) {
      this.connection
        .invoke("SendMessage", JSON.stringify(newSettings))
        .catch(function(err) {
          this.connect();
          return console.error(err.toSting());
        });
    },
    connect() {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl("/clockmaker")
        .configureLogging(signalR.LogLevel.Information)
        .build();
      this.connection.start().catch(function(err) {
        return console.error(err.toSting());
      });
    }
  },
  created() {
    const urlParams = new URLSearchParams(window.location.search);
    this.showClockOnly = urlParams.get("showClockOnly");
    this.connect();
  },
  mounted() {
    const comp = this;
    comp.connection.start();
    comp.connection.on("ReceiveMessage", messageJson => {
      comp.messages.push(messageJson);
      comp.dotMinutesSettings = JSON.parse(messageJson);
    });
  }
};
</script>
<style scoped>
.settings{
  background: #EAEAEA;
  width: 100%;
  height: 100%;
}
</style>