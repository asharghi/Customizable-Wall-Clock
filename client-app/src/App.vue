<template>
  <div class="fullsize">
    <div v-if="showClockOnly">
      <Clock
        :dotMinutesSettings="clockSettings.dotMinutesSettings"
        :dotFifthSettings="clockSettings.dotFifthSettings"
        :dotQuarterSettings="clockSettings.dotQuarterSettings"
        :watchFaceSettings="clockSettings.watchFaceSettings"
      />
    </div>
    <div v-else class="settings">
      <SettingsActionBar
        @back="displaySettingForProperty = ''"
        :previous="!displaySettingForProperty? '': 'Tilbake'"
        :current="!displaySettingForProperty? 'Innstillinger': displaySettingForProperty"
      />
      <transition :name="displaySettingForProperty ? 'slide' : 'slideback'">
        <div class="root-settings" v-show="displaySettingForProperty">
          <DotSettingsBuilder
            v-for="dotSetting in dotSettings"
            :key="dotSetting.name"
            v-show="displaySettingForProperty === dotSetting.label"
            :settings="clockSettings[dotSetting.name]"
            @change="clockSettingsPropertyChanged(dotSetting.name, $event)"
          />
          <WatchFaceBuilder
            v-show="displaySettingForProperty === 'Urskive'"
            :settings="clockSettings.watchFaceSettings"
            @change="clockSettingsPropertyChanged('watchFaceSettings', $event)"
          />
        </div>
      </transition>
      <transition :name="displaySettingForProperty ? 'slide' : 'slideback'">
        <div class="root-settings" v-show="!displaySettingForProperty">
          <div class="empty-settings-row"></div>
          <div
            class="settings-row settings-font darker"
            @click="displaySettingForProperty = 'Urskive'"
          >Urskive</div>
          <div class="empty-settings-row"></div>
          <div
            v-for="dotSetting in dotSettings"
            :key="dotSetting.name"
            class="settings-row settings-font darker"
            @click="displaySettingForProperty = dotSetting.label"
          >
            {{dotSetting.label}}
            <div class="peek-setting">{{clockSettings[dotSetting.name].active ? 'På' : 'Av'}}</div>
            <div class="edit-icon"></div>
          </div>
          <div class="empty-settings-row"></div>
          <div class="settings-row settings-font darker">Time-peker</div>
          <div class="settings-row settings-font darker">Minutt-peker</div>
          <div class="settings-row settings-font darker">Sekund-peker</div>
        </div>
      </transition>
    </div>
  </div>
</template>

<script>
import Clock from "./components/Clock.vue";
import SettingsActionBar from "./components/SettingsActionBar.vue";
import DotSettingsBuilder from "./components/DotSettingsBuilder.vue";
import WatchFaceBuilder from "./components/WatchFaceBuilder.vue";
const signalR = require("@aspnet/signalr");

export default {
  components: {
    Clock,
    SettingsActionBar,
    DotSettingsBuilder,
    WatchFaceBuilder
  },
  data() {
    return {
      displaySettingForProperty: "",
      showClockOnly: false,
      connection: "",
      dotSettings: [
        { label: "Hvert element", name: "dotMinutesSettings" },
        { label: "Hvert femte element", name: "dotFifthSettings" },
        { label: "Hvert femtene element", name: "dotQuarterSettings" }
      ],
      clockSettings: {
        dotMinutesSettings: {
          active: false,
          width: 1,
          height: 1,
          hue: 0,
          luminosity: 50
        },
        dotFifthSettings: {
          active: false,
          width: 1,
          height: 1,
          hue: 0,
          luminosity: 50
        },
        dotQuarterSettings: {
          active: false,
          width: 1,
          height: 1,
          hue: 0,
          luminosity: 50
        },
        watchFaceSettings: { size: 700, hue: 50, luminosity: 50 }
      }
    };
  },
  methods: {
    clockSettingsPropertyChanged(propertyName, newSettings) {
      this.clockSettings[propertyName] = newSettings;
      this.connection
        .invoke("SendMessage", JSON.stringify(this.clockSettings))
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
      comp.clockSettings = JSON.parse(messageJson);
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
.darker {
  color: #1c1c1e;
}
.root-settings {
  display: inline-block;
  position: absolute;
  width: 100%;
}
.edit-icon {
  width: 8px;
  height: 8px;
  border-left: 2px solid #d8d8d8;
  border-bottom: 2px solid #d8d8d8;
  transform: rotate(225deg);
  border-radius: 2px;
}
.peek-setting {
  color: #5c5c5c;
  position: absolute;
  right: 40px;
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