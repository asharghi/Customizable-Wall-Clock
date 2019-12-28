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
      <SettingsActionBar
        @back="settingsPage = ''"
        :previous="!settingsPage? '': 'Tilbake'"
        :current="!settingsPage? 'Innstillinger': settingsPage"
      />
      <transition :name="settingsPage ? 'slide' : 'slideback'">
        <DotSettingsBuilder
          v-show="settingsPage === 'Minutt-element'"
          :settings="dotMinutesSettings"
          @change="dotMinutesSettingsChanged"
        />
      </transition>
      <transition :name="settingsPage ? 'slide' : 'slideback'">
        <div class="root-settings" v-show="!settingsPage" @click="settingsPage = 'Minutt-element'">
          <div class="empty-settings-row"></div>
          <div class="settings-row settings-font">Minutt-element</div>
        </div>
      </transition>
    </div>
  </div>
</template>

<script>
import Clock from "./components/Clock.vue";
import SettingsActionBar from "./components/SettingsActionBar.vue";
import DotSettingsBuilder from "./components/DotSettingsBuilder.vue";
const signalR = require("@aspnet/signalr");

export default {
  components: {
    Clock,
    SettingsActionBar,
    DotSettingsBuilder
  },
  data() {
    return {
      settingsPage: "",
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
<style>
.settings {
  background: #eaeaea;
  width: 100%;
  height: 100%;
}
.settings-row {
  padding: 10px 20px 10px 20px;
  background: #ffffff;
  border-bottom: 1px solid #d8d8d8;
  display: flex;
  align-items: center;
  justify-content: space-between;
  cursor: pointer;
}
.empty-settings-row {
  background: #eaeaea;
  width: 100%;
  height: 20px;
  border-bottom: 1px solid #d8d8d8;
}
.settings-font {
  font-family: sans-serif;
  color: #5c5c5c;
  font-weight: 100;
  font-size: 14px;
}
.root-settings {
  display: inline-block;
  position: absolute;
  width: 100%;
}
.slide-leave-active,
.slide-enter-active,
.slideback-leave-active,
.slideback-enter-active {
  transition: 0.2s;
}
.slide-enter,
.slideback-leave-to {
  transform: translate(100%, 0);
}
.slide-leave-to,
.slideback-enter {
  transform: translate(-100%, 0);
}
</style>