<template>
  <div class="fullsize">
    <div v-if="showClockOnly">
      <Clock
        v-if="!clockSettings.watchFaceSettings.userolex"
        :dotMinutesSettings="clockSettings.dotMinutesSettings"
        :dotFifthSettings="clockSettings.dotFifthSettings"
        :dotQuarterSettings="clockSettings.dotQuarterSettings"
        :watchFaceSettings="clockSettings.watchFaceSettings"
        :secondPointerSettings="clockSettings.secondPointerSettings"
        :minutePointerSettings="clockSettings.minutePointerSettings"
        :hourPointerSettings="clockSettings.hourPointerSettings"
        :fontHourSettings="clockSettings.fontHourSettings"
        :fontQuarterSettings="clockSettings.fontQuarterSettings"
      />
      <Rolex v-else :watchFaceSettings="clockSettings.watchFaceSettings" />
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
          <PointerBuilder
            v-for="pointerSetting in pointerSettings"
            :key="pointerSetting.name"
            v-show="displaySettingForProperty === pointerSetting.label"
            :settings="clockSettings[pointerSetting.name]"
            @change="clockSettingsPropertyChanged(pointerSetting.name, $event)"
          />
          <FontBuilder
            v-for="fontSetting in fontSettings"
            :key="fontSetting.name"
            v-show="displaySettingForProperty === fontSetting.label"
            :settings="clockSettings[fontSetting.name]"
            @change="clockSettingsPropertyChanged(fontSetting.name, $event)"
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
          <RootSettingRowGroup
            :rows="dotSettings"
            :clockSettings="clockSettings"
            @selected="displaySettingForProperty = $event"
          />
          <div class="empty-settings-row"></div>
          <RootSettingRowGroup
            :rows="pointerSettings"
            :clockSettings="clockSettings"
            @selected="displaySettingForProperty = $event"
          />
          <div class="empty-settings-row"></div>
          <RootSettingRowGroup
            :rows="fontSettings"
            :clockSettings="clockSettings"
            @selected="displaySettingForProperty = $event"
          />
        </div>
      </transition>
    </div>
  </div>
</template>

<script>
import Clock from "./components/Clock.vue";
import Rolex from "./components/Rolex.vue";
import SettingsActionBar from "./components/SettingsActionBar.vue";
import DotSettingsBuilder from "./components/DotSettingsBuilder.vue";
import WatchFaceBuilder from "./components/WatchFaceBuilder.vue";
import PointerBuilder from "./components/PointerBuilder.vue";
import FontBuilder from "./components/FontBuilder.vue";
import RootSettingRowGroup from "./components/RootSettingRowGroup.vue";
const signalR = require("@aspnet/signalr");

export default {
  components: {
    Clock,
    Rolex,
    SettingsActionBar,
    DotSettingsBuilder,
    WatchFaceBuilder,
    PointerBuilder,
    FontBuilder,
    RootSettingRowGroup
  },
  data() {
    return {
      displaySettingForProperty: "",
      showClockOnly: false,
      connection: "",
      fontSettings: [
        { label: "Tekststil per time", name: "fontHourSettings" },
        { label: "Tekststil per tredje time", name: "fontQuarterSettings" }
      ],
      dotSettings: [
        { label: "Hvert element", name: "dotMinutesSettings" },
        { label: "Hvert femte element", name: "dotFifthSettings" },
        { label: "Hvert femtene element", name: "dotQuarterSettings" }
      ],
      pointerSettings: [
        { label: "Time-peker", name: "hourPointerSettings" },
        { label: "Minutt-peker", name: "minutePointerSettings" },
        { label: "Sekund-peker", name: "secondPointerSettings" }
      ],
      clockSettings: {
        dotMinutesSettings: {
          space: 0,
          width: 0,
          height: 0,
          radius: 0,
          hue: 0,
          luminosity: 50,
          active: false
        },
        dotFifthSettings: {
          space: 0,
          width: 0,
          height: 0,
          radius: 0,
          hue: 0,
          luminosity: 50,
          active: false
        },
        dotQuarterSettings: {
          space: 0,
          width: 0,
          height: 0,
          radius: 0,
          hue: 0,
          luminosity: 50,
          active: false
        },
        secondPointerSettings: {
          width: 300,
          height: 5,
          hue: 360,
          luminosity: 50,
          radius: 0,
          active: false
        },
        minutePointerSettings: {
          width: 300,
          height: 15,
          hue: 360,
          luminosity: 50,
          radius: 0,
          active: false
        },
        hourPointerSettings: {
          width: 150,
          height: 15,
          hue: 360,
          luminosity: 50,
          radius: 0,
          active: false
        },
        fontHourSettings: {
          active: true,
          fontface: "Abel",
          size: 40,
          hue: 360,
          luminosity: 50,
          space: 0
        },
        fontQuarterSettings: {
          active: false,
          fontface: "Abel",
          size: 40,
          hue: 360,
          luminosity: 50,
          space: 0
        },
        watchFaceSettings: {
          size: 500,
          left: 0,
          top: 0,
          useimage: false,
          userolex: false,
          hue: 50,
          luminosity: 50,
          imagepath: ""
        }
      }
    };
  },
  methods: {
    clockSettingsPropertyChanged(propertyName, newSettings) {
      const comp = this;
      this.clockSettings[propertyName] = newSettings;
      this.connection
        .invoke("SendMessage", JSON.stringify(this.clockSettings))
        .catch(function() {
          comp.connect();
        });
    },
    connect() {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl("/clockmaker")
        .configureLogging(signalR.LogLevel.Information)
        .build();
      this.connection.start().catch(function() {});
    }
  },
  created() {
    this.showClockOnly = window.location.pathname.split("/").includes("clock");
    var storedSettings = localStorage.getItem("storedSettings");
    if (storedSettings) this.clockSettings = JSON.parse(storedSettings);
    this.connect();
  },
  mounted() {
    const comp = this;
    comp.connection.on("ReceiveMessage", messageJson => {
      comp.clockSettings = JSON.parse(messageJson);
      localStorage.setItem("storedSettings", messageJson);
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
  padding: 20px;
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

@font-face {
  font-family: "Abel";
  src: url("/fonts/Abel.ttf");
}
@font-face {
  font-family: "AbrilFatface";
  src: url("/fonts/AbrilFatface.ttf");
}
@font-face {
  font-family: "Anton";
  src: url("/fonts/Anton.ttf");
}
@font-face {
  font-family: "Bitter";
  src: url("/fonts/Bitter.ttf");
}
@font-face {
  font-family: "CormorantGaramond";
  src: url("/fonts/CormorantGaramond.ttf");
}
@font-face {
  font-family: "Geek";
  src: url("/fonts/Geek.ttf");
}
@font-face {
  font-family: "Kalam";
  src: url("/fonts/Kalam.ttf");
}
@font-face {
  font-family: "Lobster";
  src: url("/fonts/Lobster.ttf");
}
@font-face {
  font-family: "Monoton";
  src: url("/fonts/Monoton.ttf");
}
@font-face {
  font-family: "OldStandardTT";
  src: url("/fonts/OldStandardTT.ttf");
}
@font-face {
  font-family: "Orbitron";
  src: url("/fonts/Orbitron.ttf");
}
@font-face {
  font-family: "PTSans";
  src: url("/fonts/PTSans.ttf");
}
</style>
