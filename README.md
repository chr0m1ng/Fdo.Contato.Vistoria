# Fdo.Contato.Vistoria

## Overview
This Xamarin Forms App upload images to the [Contato Vistoria API](https://github.com/chr0m1ng/Fdo.Api.Contato.Vistoria).

The aim of this project is to help the car inspection image uploading process, you just need to provide the vehicle plates, take a photo or choose from the gallery
and the app will upload it to the server.

## UI

A gif explain more than a thousand words!

APP_WORKING_GIF

But just to be sure, a briefly explaination:

The user can inform the vehicle plate and start to inspect the vehicle on the `NewVehiclePage`, at the `VehiclePage` the user can take a new photo or even
add a few from the phone's gallery. All photos will be saved on the phone with a new folder named with the vehicle plate, while adding new photos the app will be uploading
in the background and inform if it successfully uploaded or if some error occured, in case of an error the user can retry all error images or just one.
Users also can take a better look at the pictures, they just need to tap the thumbnail of the desired picture and a `ImageViewerPage` will appear with the possibility of zoom in the image.

Our app also provide the possibility of changing the server API url and the `x-api-key` header sent to the server at uploading process,
to do so the user just need to navigate to the `ConfigurationPag` by clicking the **Configuration Tab**. To go back to inspect just click back on the **Inspect Tab**.

## Installation

Just go to [Google Play Store](https://play.google.com/store/apps/details?id=com.x4devsonly.contato.vistoria) and download it to test.

## Tests

A few UI tests following the [Page Object Pattern](https://www.xamarinhelp.com/page-object-pattern-will-make-better-xamarin-ui-automation-tester/) are available at [Tests Project](Fdo.Contato.Vistoria.UITest).
